using System;
using Microsoft.ML;

namespace Router.Core.Services.Implementations;

internal class PredictionService
{
    public static string ModelPath => Path.Combine();
    private readonly MLContext _context;
    private ITransformer? _model;

    public PredictionService(MLContext context) => _context = context;

    public async Task<bool> PredictAsync(MLRequest<PIIDataDTO> request)
    {
        LoadModel();
        var predictionEngine = _context.Model.CreatePredictionEngine<PIIData, PIIPrediction>(_model);
        
        return await Task.FromResult(new MLResponse<PIIPrediction>
        {
            Data = predictionEngine.Predict(request.Data!.Map())
        });
    }

    private void LoadModel()
    {
        using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            _model = _context.Model.Load(stream, out _);
        }
    }
}
