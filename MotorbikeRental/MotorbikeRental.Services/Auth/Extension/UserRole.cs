namespace MotorbikeRental.Services.Auth.Extension
{
    public static class UserRole
    {
        public static string GetUserRole(int userTypeId)
        {
            switch (userTypeId)
            {
                case 1:
                    return "admin";
                case 2:
                    return "user";
                default:
                    return "";
            }
        }
    }
}
