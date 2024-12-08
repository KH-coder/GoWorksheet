using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using GoWorksheet.Infrastructure.Services.Interfaces;
using GoWorksheet.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace GoWorksheet.Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        private readonly IWorksheetRepository _worksheetRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<PdfService> _logger;

        public PdfService(
            IWorksheetRepository worksheetRepository,
            IFileStorageService fileStorageService,
            ILogger<PdfService> logger)
        {
            _worksheetRepository = worksheetRepository;
            _fileStorageService = fileStorageService;
            _logger = logger;
        }

        public async Task<byte[]> GenerateWorksheetPdfAsync(int worksheetId)
        {
            try
            {
                var worksheet = await _worksheetRepository.GetWithProblemsAsync(worksheetId);
                if (worksheet == null)
                {
                    throw new KeyNotFoundException($"Worksheet with ID {worksheetId} not found.");
                }

                using (var document = CreateWorksheetDocument(worksheet))
                {
                    return document.GeneratePdf();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating PDF for worksheet {WorksheetId}", worksheetId);
                throw;
            }
        }

        public async Task<string> SaveWorksheetPdfAsync(int worksheetId)
        {
            var pdfBytes = await GenerateWorksheetPdfAsync(worksheetId);
            using (var memoryStream = new MemoryStream(pdfBytes))
            {
                return await _fileStorageService.SaveFileAsync(
                    memoryStream, 
                    $"worksheet_{worksheetId}.pdf"
                );
            }
        }

        private IDocument CreateWorksheetDocument(Worksheet worksheet)
        {
            return Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Element(container =>
                    {
                        container.Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item().Text(worksheet.Title)
                                    .SemiBold().FontSize(20);
                                
                                if (!string.IsNullOrEmpty(worksheet.Description))
                                {
                                    column.Item().Text(worksheet.Description)
                                        .FontSize(12).FontColor(Colors.Grey.Medium);
                                }
                            });
                        });
                    });

                    page.Content().Element(container =>
                    {
                        container.PaddingVertical(1, Unit.Centimetre);
                        
                        var orderedProblems = worksheet.Problems
                            .OrderBy(wp => wp.ProblemOrder)
                            .ToList();

                        foreach (var worksheetProblem in orderedProblems)
                        {
                            container.Row(row =>
                            {
                                row.AutoItem().Text($"{worksheetProblem.ProblemOrder}.")
                                    .Width(20).SemiBold();
                                
                                row.RelativeItem().Column(column =>
                                {
                                    if (!string.IsNullOrEmpty(worksheetProblem.Problem.ImagePath))
                                    {
                                        // 处理图像显示
                                        column.Item().Image(worksheetProblem.Problem.ImagePath)
                                            .FitArea(400, 300);
                                    }

                                    column.Item().Text($"Type: {worksheetProblem.Problem.Type.Name}")
                                        .FontSize(10).FontColor(Colors.Grey.Medium);
                                    
                                    column.Item().Text($"Difficulty: {worksheetProblem.Problem.Difficulty.Name}")
                                        .FontSize(10).FontColor(Colors.Grey.Medium);
                                });
                            });
                        }
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Page ");
                        text.CurrentPageNumber();
                        text.Span(" of ");
                        text.TotalPages();
                    });
                });
            });
        }
    }
}