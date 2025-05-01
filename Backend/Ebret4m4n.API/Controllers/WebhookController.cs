using Ebret4m4n.Entities.ConfigurationModels;
using Microsoft.Extensions.Options;
using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Ebret4m4n.Shared.DTOs;
using Ebret4m4n.Contracts;
using Stripe.Checkout;
using Stripe;
using Mapster;


namespace Ebret4m4n.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebhookController
    (IUnitOfWork unitOfWork,
    IOptions<StripeSettings> stripeConfig,
    IEmailSender emailSender) : ControllerBase
{
    readonly StripeSettings _stripConfig = stripeConfig.Value;

    [HttpPost("stripe")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent =
            EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"],
            _stripConfig.WebhookSecret);

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;

                if (session != null &&
                    session.Mode == "payment" &&
                    session.PaymentStatus == "paid")
                {
                    var childId = session.Metadata["childId"];
                    var childName = session.Metadata["childName"];
                    var parentEmail = session.Metadata["parentEmail"];

                    var transaction = (session, session.ClientReferenceId, childId).Adapt<Transaction>();

                    transaction.PaymentIntentId = session.PaymentIntentId;
                    transaction.Paid = true;
                    transaction.PaidAt = DateTime.UtcNow;

                    await unitOfWork.TransactionRepo.AddAsync(transaction);

                    var result = await unitOfWork.SaveAsync();

                    if (result == 0)
                        return StatusCode(StatusCodes.Status500InternalServerError);

                    await emailSender.SendEmailAsync(parentEmail, "عمليه الدفع", $"<p>عمليه دفع ناجحه للطفل : {childName}</p>");
                }
            }
        }
        catch (StripeException ex)
        {
            return BadRequest(GeneralResponse<string>.FailureResponse($"stripe webhook error {ex.Message}"));
        }

        return Ok();
    }
}
