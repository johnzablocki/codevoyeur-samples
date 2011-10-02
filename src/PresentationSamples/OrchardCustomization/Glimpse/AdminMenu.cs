using Orchard.Localization;
using Orchard.UI.Navigation;
using Orchard.Core.Navigation;

namespace Glimpse {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }

        public AdminMenu() {
            T = NullLocalizer.Instance;
        }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Glimpse"), "7",
                    menu => menu.Add(T("Glimpse"), "0", item => item.Action("Index", "Admin", new { area = "Glimpse" })
                        .Permission(Permissions.ManageMainMenu)));
        }
    }
}