using System;

using Microsoft.AspNetCore.Components;

namespace WebServiceGilBT.Shared {
    public partial class PresentationEditor : ComponentBase {
        [Parameter]
        public Pres Pres { set; get; }

        //element edit
        public void AddElement(Page p) {
            PageElement pe = new PageElement("Tekst", 0, 0, 1, FontType.fontnormal8px);
            p.elements.Add(pe);
        }

        public void OnIdxChanched(ChangeEventArgs e) {
            Console.WriteLine("Idxchanged");
            StateHasChanged();
        }

        public void DeletePage(Page p) {
            Pres.pages.Remove(p);
        }

        //page edit
        public void AddPageClicked() {
            Page p = new Page(2);
            Pres.pages.Add(p);
        }

        public void DeleteElement(Page p, PageElement pe) {
            p.elements.Remove(pe);
        }
        private void HandleValidSubmit() {
            Console.WriteLine("OnValidSubmit");
        }
    }
}
