﻿using CommunityToolkit.Mvvm.Input;
using Globomantics.Domain;
using Globomantics.Infrastructure.Data.Repositories;
using System.Threading.Tasks;

namespace Globomantics.Windows.ViewModels
{
    public class FeatureViewModel : BaseTodoViewModel<Feature>
    {
        private readonly IRepository<Feature> repository;
        private string? description;
        public string? Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public FeatureViewModel(IRepository<Feature> repository) : base()
        {
            this.repository = repository;

            SaveCommand = new RelayCommand(async () => await SaveAsync());
        }
        public override async Task SaveAsync()
        {
            if(string.IsNullOrWhiteSpace(Title))
            {
                ShowError?.Invoke($"{nameof(Title)} cannot be empty");
                return;
            }

            if (Model is null)
            {
                Model = new Feature(Title, Description, "UI?", 1,
                    App.CurrentUser, App.CurrentUser)
                {
                    DueDate = System.DateTimeOffset.Now.AddDays(10),
                    Parent = Parent,
                    IsCompleted = IsCompleted
                };
            }
            else
            {
                Model = Model with
                {
                    Title = Title,
                    Description = Description,
                    Parent = Parent,
                    IsCompleted = IsCompleted
                };
            }

            await repository.AddAsync(Model);
            await repository.SaveChangesAsync();

            // TODO: Send a message that an item has been saved
        }

        public override void UpdateModel(Todo model)
        {
            if (model is not Feature feature) return;

            base.UpdateModel(feature);

            Description = feature.Description;
        }
    }
}
