export interface StatisticsAdmin {
  healthCareUnits: number;
  registeredChildren: number;
  fullyVaccinatedChildren: number;
  totalVaccinesTaken: number;
  totalComplaints: number;
  maleChildren: number;
  femaleChildren: number;
  malePercentage: number;
  femalePercentage: number;
  topGovernoratesByVaccines: [
    {
      governorate: string;
      totalVaccinesRequested: number;
    }
  ];
  governoratesReport: [
    {
      governorate: string;
      cityCount: number;
      healthUnitCount: number;
      vaccinesTaken: number;
    }
  ];
  vaccineRequests: [
    {
      vaccineName: string;
      requestedAmount: number;
    }
  ];
}
