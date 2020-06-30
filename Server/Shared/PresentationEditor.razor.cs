using System;

using Microsoft.AspNetCore.Components;

namespace WebServiceGilBT.Shared {
    public partial class PresentationEditor : ComponentBase {
        [Parameter]
        public Pres Pres { set; get; }

        public void AddPageClicked() {

            Page p = new Page(2);
            Pres.pages.Add(p);
            Console.WriteLine("AddPageClicked");
        }

        public void DeletePage(Page p) {
			Console.WriteLine("removing page");
        }

        public void AddElement(Page p) {
			Console.WriteLine("Add element");
        }

        public void DeleteElement(PageElement pe) {
			Console.WriteLine("removing element {0}", pe.ToString());
        }
    }
}
