using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Services {
    public class PreviewService {
        Canvas2DContext _outputCanvasContext;
        Pres prezentacja;
        BECanvasComponent _canvasReference = null;
        int skala;

        public bool rysowanieWToku = true;

        public PreviewService(Canvas2DContext context, Pres przntcja, BECanvasComponent Canvas, int skala) {
            _outputCanvasContext = context;
            prezentacja = przntcja;
            _canvasReference = Canvas;
            this.skala = skala;
        }

        string fontCode(FontType f) {
            switch (f) {
                case FontType.arial14:
                    return $"{skala * 14}px arial";
                case FontType.arial16:
                    return $"{skala * 16}px arial";
                case FontType.impact14:
                    return $"{skala * 14}px impact";
                case FontType.impact16:
                    return $"{skala * 16}px impact";
                case FontType.fontfat8px:
                    return $"bold {skala * 8}px Courier New";
                case FontType.fontnormal8px:
                default:
                    return $"{skala * 8}px Courier New";
            }
        }

        async ValueTask drawPage(Page p) {
            await _outputCanvasContext.ClearRectAsync(0, 0, _canvasReference.Width, _canvasReference.Height);
            await _outputCanvasContext.SetFillStyleAsync("black");
            await _outputCanvasContext.FillRectAsync(0, 0, _canvasReference.Width, _canvasReference.Height);
            foreach (PageElement e in p.elements) {
                await _outputCanvasContext.SetFontAsync(fontCode(e.font));
                await _outputCanvasContext.SetFillStyleAsync("#ffffff");
                await _outputCanvasContext.FillTextAsync(e.text, e.x * skala, e.y * skala);
            }
            await Task.Delay(p.time * 1000);
        }

        public async Task drawAllPages() {
            while (rysowanieWToku) {
                foreach (Page p in prezentacja.pages) {
                    await drawPage(p);
                }
            }
        }

    }
}
