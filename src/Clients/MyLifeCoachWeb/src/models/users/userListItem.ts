export interface UserListItem {
  id: string;
  userId: string;
  fullName: string;
  username: string;
  email: string;
  isActive: boolean;
  deactivationDate: Date | null;
}
