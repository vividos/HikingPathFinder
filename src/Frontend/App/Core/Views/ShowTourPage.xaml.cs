﻿using HikingPathFinder.App.ViewModels;
using HikingPathFinder.Model;
using System.Diagnostics;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Shows a single tour
    /// </summary>
    public partial class ShowTourPage : ContentPage
    {
        /// <summary>
        /// Creates a new page to show the given tour
        /// </summary>
        /// <param name="tour">tour to show</param>
        public ShowTourPage(Tour tour)
        {
            Debug.Assert(tour != null, "tour must be non-null");

            this.InitializeComponent();

            this.BindingContext = new ShowTourViewModel(tour);
            this.tourSummary.BindingContext = new TourSummaryViewModel(tour);
        }
    }
}
