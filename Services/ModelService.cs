using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Services
{
    public class ModelService
    {
        private readonly IMiviaClient _client;
        public List<ModelDto>? _models;
        private DateTime _lastUpdateTime;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMinutes(30);

        public ModelService (IMiviaClient miviaClient) 
        {
            _client = miviaClient;
        }

        public async Task<List<ModelDto>> GetModelsAsync()
        {
            if (_models == null || DateTime.Now - _lastUpdateTime > _updateInterval)
            {
                _models = await _client.GetModelsAsync();
                _lastUpdateTime = DateTime.Now;
            }

            return _models;
        }
    }
}
