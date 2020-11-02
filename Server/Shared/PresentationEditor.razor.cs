using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Constraints;
using WebServiceGilBT.Data;
using WebServiceGilBT.Services;

namespace WebServiceGilBT.Shared {
    public partial class PresentationEditor : ComponentBase, IDisposable {
        [Parameter]
        public Pres Pres { set; get; }

        [Parameter]
        public Screen scr { get; set; }

        [Parameter]
        public User loggeduser { get; set; }

        [Inject]
        Lang lng { set; get; }

        UniversalMysqlService<PresTemplate> presTemplateService;
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

        bool chooseTeamplate { set; get; } = false;
        public void AddNameTemplateClicked() {
            chooseTeamplate = false;
            Page p = new Page(10);
            p.elements.Add(PageElement.NewText(scr.name, 0, 0, 1, FontType.fontnormal8px));
            p.elements.Add(PageElement.NewUid(0, 8, 1, FontType.fontfat8px));
            Pres.pages.Add(p);
        }

        public void AddDateTimeTemplateClicked() {
            chooseTeamplate = false;
            Page p = new Page(10);
            p.elements.Add(PageElement.NewText("Godzina Data", 0, 0, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewTime(0, 8, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewDate(40, 8, 1, FontType.fontfat8px));
            Pres.pages.Add(p);
        }

        public void AddPM10TemplateClicked() {
            chooseTeamplate = false;
            Page p = new Page(10);
            p.elements.Add(PageElement.NewText("Pm10", 0, 0, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewSensorPm10(Pres.UnifiedIdx, 35, 0, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewText("Pm10", 0, 8, 1, FontType.fontfat8px));
            PageElement pe = PageElement.NewSensorPm10(Pres.UnifiedIdx, 35, 8, 1, FontType.fontfat8px);
            pe.type = ElementType.SENSOR_PM10_PERCENT;
            p.elements.Add(pe);
            Pres.pages.Add(p);
        }

        public void AddPM2_5TemplateClicked() {
            chooseTeamplate = false;
            Page p = new Page(10);
            p.elements.Add(PageElement.NewText("Pm2.5", 0, 0, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewSensorPm2_5(Pres.UnifiedIdx, 35, 0, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewText("Pm2.5", 0, 8, 1, FontType.fontfat8px));
            PageElement pe = PageElement.NewSensorPm2_5(Pres.UnifiedIdx, 35, 8, 1, FontType.fontfat8px);
            pe.type = ElementType.SENSOR_PM2_5_PERCENT;
            p.elements.Add(pe);
            Pres.pages.Add(p);
        }



        public void AddPreassureTempTemplateClicked() {
            chooseTeamplate = false;
            Page p = new Page(10);
            p.elements.Add(PageElement.NewText("Ciśnie.", 0, 0, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewSensorPressure(Pres.UnifiedIdx, 35, 0, 1, FontType.fontfat8px));
            p.elements.Add(PageElement.NewText("Temp.", 0, 8, 1, FontType.fontfat8px));
            PageElement pe = PageElement.NewSensorTemperature(Pres.UnifiedIdx, 35, 8, 1, FontType.fontfat8px);
            p.elements.Add(pe);
            Pres.pages.Add(p);
        }

        public void AddAirQualityTemplateClicked() {
            chooseTeamplate = false;
            Page p = new Page(10);
            p.elements.Add(PageElement.NewText("Jakość powietrza", 0, 0, 1, FontType.fontfat8px));
            PageElement pe = PageElement.NewSensorPm10(Pres.UnifiedIdx, 0, 8, 1, FontType.fontfat8px);
            pe.type = ElementType.SENSOR_PM10_STATUS;
            p.elements.Add(pe);
            Pres.pages.Add(p);
        }

        protected override void OnInitialized() {
            lng.LangChanged += StateHasChanged;
            base.OnInitialized();
        }

        public void Dispose() {
            lng.LangChanged -= StateHasChanged;
        }

        bool showTypeTemplateName { set; get; } = false;
        string templateName = "";
        string templateKomunikat = "";
        public async Task SaveAsTemplateClicked() {
            if (!string.IsNullOrEmpty(templateName)) {
                showTypeTemplateName = false;
                if (presTemplateService == null) presTemplateService = new UniversalMysqlService<PresTemplate>(new SqlDataAccess(null), PresTemplate.tableName, nameof(PresTemplate.Id));
                string data = DateTime.Now.ToString("G");
                PresTemplate pt = new PresTemplate() { TemplateName = templateName, Width = scr.width, Height = scr.height, prezentacja = Pres, ScreenType = scr.screen_type, UserAuthorId = loggeduser.UserId, CreateDate = data };
                await presTemplateService.PostRecordAsync(pt);
                templateKomunikat = lng.templateHasBeenSaved;
            } else {
                templateKomunikat = lng.templateNameIsEmpty;
            }
        }
    }
}
