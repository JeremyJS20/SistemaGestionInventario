using System.Reflection;

namespace SistemaGestionInventario.Enums
{
    public class UserStatusEnum
    {
        public string Code { get; private set; }
        public string Description { get; private set; }

        private UserStatusEnum(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static readonly UserStatusEnum AC = new UserStatusEnum("AC", "Activo");
        public static readonly UserStatusEnum IN = new UserStatusEnum("IN", "Inactivo");

        public override string ToString() => Description;
        public static UserStatusEnum FromCode(string code)
        {
            var fields = typeof(UserStatusEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var f in fields)
            {
                var status = f.GetValue(null) as UserStatusEnum;
                if (status != null && status.Code == code)
                    return status;
            }

            throw new ArgumentException($"No Status found with value '{code}'");
        }

        public static IList<UserStatusEnum> GetAll()
        {
            var fields = typeof(UserStatusEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static);

            var statuses = new List<UserStatusEnum>();

            foreach (var f in fields)
            {
                var status = f.GetValue(null) as UserStatusEnum;

                statuses.Add(status!);
            }

            return statuses;
        }
    }
}
