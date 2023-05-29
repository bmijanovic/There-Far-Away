﻿using Microsoft.Extensions.DependencyInjection;
using System;
using Three_Far_Away.Components;
using Three_Far_Away.Infrastructure;

namespace Three_Far_Away.ViewModels
{
    public class AgentMainViewModel : NavigableViewModel
    {
        private AgentNavigationBarViewModel _agentNavigationBarViewModel;
        public AgentNavigationBarViewModel AgentNavigationBarViewModel
        {
            get { return _agentNavigationBarViewModel; }
            set
            {
                _agentNavigationBarViewModel = value;
                OnPropertyChanged(nameof(AgentNavigationBarViewModel));
            }
        }

        public AgentMainViewModel(JourneysViewModel agentJourneysViewModel, AgentNavigationBarViewModel agentNavigationBarViewModel)
        {
            _agentNavigationBarViewModel = agentNavigationBarViewModel;
            SwitchCurrentViewModel(agentJourneysViewModel);
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            EventBus.RegisterHandler("AgentJourneys", () =>
            {
                JourneysViewModel ajvm = App.host.Services.GetRequiredService<JourneysViewModel>();
                SwitchCurrentViewModel(ajvm);
            });

            EventBus.RegisterHandler("AgentJourneyPreview", (object journeyId) =>
            {
                AgentJourneyPreviewViewModel ajpvm = new AgentJourneyPreviewViewModel((Guid)journeyId);
                SwitchCurrentViewModel(ajpvm);
            });
        }
    }
}
