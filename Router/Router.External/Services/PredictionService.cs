using System;
using Microsoft.ML;
using Router.Core.Models;
using Router.Core.Services;
using Router.Domain.MessageAggregate;

namespace Router.External.Services;

internal class PredictionService : IPredictionService
{
    public static string ModelPath => Path.Combine();
    private readonly MLContext _context;
    private ITransformer? _model;

    public PredictionService(MLContext context) => _context = context;

    public async Task<bool> PredictAsync(PIIData data)
    {
        LoadModel();
        var predictionEngine = _context.Model.CreatePredictionEngine<PIIData, PredictionResponse>(_model);
        
        PredictionResponse response = predictionEngine.Predict(data);
        
        return await Task.FromResult(response.PredictedLabel);
    }

    private void LoadModel()
    {
        using (FileStream stream = 
            new (ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            _model = _context.Model.Load(stream, out _);
        }
    }
}
