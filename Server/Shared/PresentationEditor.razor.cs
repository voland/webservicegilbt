using System;

using Microsoft.AspNetCore.Components;

namespace WebServiceGilBT.Shared {
    public partial class PresentationEditor : ComponentBase {
        [Parameter]
        public Pres Pres { set; get; }

        //element edit
        public void MoveUp(Page p) {
            int idx = Pres.pages.IndexOf(p);
            if (idx > 0) {
                Pres.pages.Remove(p);
                Pres.pages.Insert(idx - 1, p);
            }
        }

        public void MoveDown(Page p) {
            if (Pres.pages[Pres.pages.Count - 1] != p) {
                int idx = Pres.pages.IndexOf(p);
                Pres.pages.Remove(p);
                Pres.pages.Insert(idx + 1, p);
            }
        }

        public void AddElement(Page p) {
            PageElement pe = new PageElement("Tekst", 0, 0, 1, FontType.fontnormal8px);
            p.elements.Add(pe);
        }

        public void OnIdxChanched(ChangeEventArgs e) {
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
            Debuger.PrintLn("OnValidSubmit");
        }

        private int bi = 0;
        protected string item_background {
            get {
                bi++;
                if ((bi %= 2) == 1) {
                    return "my_custom_th_light";
                } else {
                    return "my_custom_th_dark";
                }
            }
        }

    }
}
