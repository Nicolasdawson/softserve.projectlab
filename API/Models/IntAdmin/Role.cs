using API.Models.IntAdmin.Interfaces;

namespace API.Models.IntAdmin
{
    public class Role : IRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Role(int roleId, string roleName, string description, string status)
        {
            RoleId = roleId;
            RoleName = roleName;
            Description = description;
            Status = status;
        }

        // Constructor por defecto para escenarios de serialización u otras necesidades.
        public Role() { }

        public Result<IRole> AddRole(IRole role)
        {
            // Lógica para agregar un nuevo rol (por ejemplo, guardar en base de datos o colección)
            return Result<IRole>.Success(role);
        }

        public Result<IRole> UpdateRole(IRole role)
        {
            // Lógica para actualizar un rol existente
            return Result<IRole>.Success(role);
        }

        public Result<IRole> GetRoleById(int roleId)
        {
            // Lógica para obtener un rol por su ID
            var role = new Role(roleId, "Administrador", "Rol de administrador", "Activo");
            return Result<IRole>.Success(role);
        }

        public Result<List<IRole>> GetAllRoles()
        {
            // Lógica para obtener todos los roles
            var roles = new List<IRole>
            {
                new Role(1, "Administrador", "Rol de administrador", "Activo"),
                new Role(2, "Usuario", "Rol de usuario", "Activo")
            };
            return Result<List<IRole>>.Success(roles);
        }

        public Result<bool> RemoveRole(int roleId)
        {
            // Lógica para eliminar un rol por su ID
            return Result<bool>.Success(true);
        }
    }
}
