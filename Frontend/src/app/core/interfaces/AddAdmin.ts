export interface AddAdmin {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  governorate?: string;
  city?: string;
  healthCareCenterId?: string;
  staffRole?: string;
}
