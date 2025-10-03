namespace BookStore.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<AdminPermission> Permissions { get; set; } = null!;
    }

    public enum AdminPermission
    {
        ViewOrders,
        ManageOrders,
        CancelOrders,

        ViewProducts,
        ManageProducts,
        ManageCatalog,

        ViewCustomers,
        ViewAdmins,
        ManageAdmins,
        ViewAdminActions,

        ViewDiscounts,
        ManageDiscounts,

        ManageReviews
    }
}