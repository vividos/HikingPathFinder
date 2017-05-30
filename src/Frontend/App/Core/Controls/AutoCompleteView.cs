/// <copyright file="AutoCompleteView.cs" company="XLabs Team">
///     Copyright (c) XLabs Team. All rights reserved.
///     Copyright (c) 2017 Michael Fink.
/// </copyright>
/// <summary>
///       This project is licensed under the Apache 2.0 license
///       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
///
///       XLabs is a open source project that aims to provide a powerfull and cross
///       platform set of controls tailored to work with Xamarin Forms.
/// </summary>
namespace HikingPathFinder.App.Controls
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Xamarin.Forms;

    /// <summary>
    /// Provides an entry control with optional search button that shows suggestions for auto-
    /// completion of the entry, based on previously typed text. Many aspects of the control can
    /// be configured using properties.
    /// </summary>
    public class AutoCompleteView : ContentView
    {
        #region Bindable properties
        #pragma warning disable CS0618 // Type or member is obsolete
        /// <summary>
        /// Property that indicates if suggestion clicks should result in an "execute" of the
        /// search; allowed values: True, False.
        /// </summary>
        public static readonly BindableProperty ExecuteOnSuggestionClickProperty =
            BindableProperty.Create<AutoCompleteView, bool>(p => p.ExecuteOnSuggestionClick, false);

        /// <summary>
        /// Property to store placeholder text for text entry control.
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create<AutoCompleteView, string>(p => p.Placeholder, string.Empty, BindingMode.TwoWay, null, OnPlaceholderChanged);

        /// <summary>
        /// Property to store background color for the search button.
        /// </summary>
        public static readonly BindableProperty SearchBackgroundColorProperty =
            BindableProperty.Create<AutoCompleteView, Color>(p => p.SearchBackgroundColor, Color.Red, BindingMode.TwoWay, null, OnSearchBackgroundColorChanged);

        /// <summary>
        /// Property to store the border color for the search button.
        /// </summary>
        public static readonly BindableProperty SearchBorderColorProperty =
            BindableProperty.Create<AutoCompleteView, Color>(p => p.SearchBorderColor, Color.White, BindingMode.TwoWay, null, OnSearchBorderColorChanged);

        /// <summary>
        /// Property to store the border radius for the search button.
        /// </summary>
        public static readonly BindableProperty SearchBorderRadiusProperty =
            BindableProperty.Create<AutoCompleteView, int>(p => p.SearchBorderRadius, 0, BindingMode.TwoWay, null, OnSearchBorderRadiusChanged);

        /// <summary>
        /// Property to store the border width for the search button.
        /// </summary>
        public static readonly BindableProperty SearchBorderWidthProperty =
            BindableProperty.Create<AutoCompleteView, int>(p => p.SearchBorderWidth, 1, BindingMode.TwoWay, null, OnSearchBorderWidthChanged);

        /// <summary>
        /// Property to store the search command that is sent when the search button is pressed.
        /// </summary>
        public static readonly BindableProperty SearchCommandProperty =
            BindableProperty.Create<AutoCompleteView, ICommand>(p => p.SearchCommand, null);

        /// <summary>
        /// Property to store the horizontal options for the search button.
        /// </summary>
        public static readonly BindableProperty SearchHorizontalOptionsProperty =
            BindableProperty.Create<AutoCompleteView, LayoutOptions>(p => p.SearchHorizontalOptions, LayoutOptions.FillAndExpand, BindingMode.TwoWay, null, OnSearchHorizontalOptionsChanged);

        /// <summary>
        /// Property to store the text color for the search button.
        /// </summary>
        public static readonly BindableProperty SearchTextColorProperty =
            BindableProperty.Create<AutoCompleteView, Color>(p => p.SearchTextColor, Color.Red, BindingMode.TwoWay, null, OnSearchTextColorChanged);

        /// <summary>
        /// Property to store the text for the search button.
        /// </summary>
        public static readonly BindableProperty SearchTextProperty =
            BindableProperty.Create<AutoCompleteView, string>(p => p.SearchText, "Search", BindingMode.TwoWay, null, OnSearchTextChanged);

        /// <summary>
        /// Property to store the vertical options for the search button.
        /// </summary>
        public static readonly BindableProperty SearchVerticalOptionsProperty =
            BindableProperty.Create<AutoCompleteView, LayoutOptions>(p => p.SearchVerticalOptions, LayoutOptions.Center, BindingMode.TwoWay, null, OnSearchVerticalOptionsChanged);

        /// <summary>
        /// Property to store the command that is sent when a suggested item has been clicked.
        /// </summary>
        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create<AutoCompleteView, ICommand>(p => p.SelectedCommand, null);

        /// <summary>
        /// Property to store the item selected from the suggestion list.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create<AutoCompleteView, object>(p => p.SelectedItem, null, BindingMode.TwoWay);

        /// <summary>
        /// Property to store if the search button should appear at all.
        /// </summary>
        public static readonly BindableProperty ShowSearchProperty =
            BindableProperty.Create<AutoCompleteView, bool>(p => p.ShowSearchButton, true, BindingMode.TwoWay, null, OnShowSearchChanged);

        /// <summary>
        /// Property to store the background color of the suggestion list.
        /// </summary>
        public static readonly BindableProperty SuggestionBackgroundColorProperty =
            BindableProperty.Create<AutoCompleteView, Color>(p => p.SuggestionBackgroundColor, Color.Red, BindingMode.TwoWay, null, OnSuggestionBackgroundColorChanged);

        /// <summary>
        /// Property to store the DataTemplate that should be used for every suggestion list item.
        /// </summary>
        public static readonly BindableProperty SuggestionItemDataTemplateProperty =
            BindableProperty.Create<AutoCompleteView, DataTemplate>(p => p.SuggestionItemDataTemplate, null, BindingMode.TwoWay, null, OnSuggestionItemDataTemplateChanged);

        /// <summary>
        /// Property to store the height request of the suggestions list.
        /// </summary>
        public static readonly BindableProperty SuggestionsHeightRequestProperty =
            BindableProperty.Create<AutoCompleteView, double>(p => p.SuggestionsHeightRequest, 250, BindingMode.TwoWay, null, OnSuggestionHeightRequestChanged);

        /// <summary>
        /// Property to store the list of (view model) items that can be presented as suggestions.
        /// </summary>
        public static readonly BindableProperty SuggestionsProperty =
            BindableProperty.Create<AutoCompleteView, IEnumerable>(p => p.Suggestions, null);

        /// <summary>
        /// Property to store the background color of the text entry element.
        /// </summary>
        public static readonly BindableProperty TextBackgroundColorProperty =
            BindableProperty.Create<AutoCompleteView, Color>(p => p.TextBackgroundColor, Color.Transparent, BindingMode.TwoWay, null, OnTextBackgroundColorChanged);

        /// <summary>
        /// Property to store the text color of the text entry element.
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<AutoCompleteView, Color>(p => p.TextColor, Color.Black, BindingMode.TwoWay, null, OnTextColorChanged);

        /// <summary>
        /// Property to store the horizontal options for the text entry element.
        /// </summary>
        public static readonly BindableProperty TextHorizontalOptionsProperty =
            BindableProperty.Create<AutoCompleteView, LayoutOptions>(p => p.TextHorizontalOptions, LayoutOptions.FillAndExpand, BindingMode.TwoWay, null, OnTextHorizontalOptionsChanged);

        /// <summary>
        /// Property to store the text that was entered into the text entry element. The text may
        /// or may not correspond to a selected item. Use SelectedItem property to identify
        /// selected item instead.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<AutoCompleteView, string>(p => p.Text, string.Empty, BindingMode.TwoWay, null, OnTextValueChanged);

        /// <summary>
        /// Property to store the vertical options for the text entry element.
        /// </summary>
        public static readonly BindableProperty TextVerticalOptionsProperty =
            BindableProperty.Create<AutoCompleteView, LayoutOptions>(p => p.TextVerticalOptions, LayoutOptions.Start, BindingMode.TwoWay, null, OnTextVerticalOptionsChanged);
#pragma warning restore CS0618 // Type or member is obsolete
        #endregion

        /// <summary>
        /// List of currently available suggestions.
        /// </summary>
        private readonly ObservableCollection<object> availableSuggestions;

        /// <summary>
        /// Search button to start searching for text
        /// </summary>
        private readonly Button searchButton;

        /// <summary>
        /// Entry element to enter text
        /// </summary>
        private readonly Entry enteredText;

        /// <summary>
        /// List view to display suggestions
        /// </summary>
        private readonly ListView suggestionsListView;

        /// <summary>
        /// Layout for edit text field and search button
        /// </summary>
        private readonly StackLayout layout;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteView"/> class.
        /// </summary>
        public AutoCompleteView()
        {
            this.availableSuggestions = new ObservableCollection<object>();
            this.layout = new StackLayout();
            var innerLayout = new StackLayout();

            this.enteredText = new Entry
            {
                HorizontalOptions = this.TextHorizontalOptions,
                VerticalOptions = this.TextVerticalOptions,
                TextColor = this.TextColor,
                BackgroundColor = this.TextBackgroundColor
            };

            this.searchButton = new Button
            {
                VerticalOptions = this.SearchVerticalOptions,
                HorizontalOptions = this.SearchHorizontalOptions,
                Text = this.SearchText
            };

            this.suggestionsListView = new ListView
            {
                HeightRequest = this.SuggestionsHeightRequest,
                HasUnevenRows = true
            };

            innerLayout.Children.Add(this.enteredText);
            innerLayout.Children.Add(this.searchButton);

            this.layout.Children.Add(innerLayout);
            this.layout.Children.Add(this.suggestionsListView);

            this.Content = this.layout;

            this.enteredText.TextChanged += (s, e) =>
            {
                this.Text = e.NewTextValue;
                this.OnTextChanged(e);
            };

            this.searchButton.Clicked += (s, e) =>
            {
                if (this.SearchCommand != null &&
                    this.SearchCommand.CanExecute(this.Text))
                {
                    this.SearchCommand.Execute(this.Text);
                }
            };

            this.suggestionsListView.ItemSelected += (s, e) =>
            {
                this.enteredText.Text = e.SelectedItem.ToString();

                this.availableSuggestions.Clear();
                this.ShowHideListbox(false);
                this.OnSelectedItemChanged(e.SelectedItem);

                if (this.ExecuteOnSuggestionClick &&
                    this.SearchCommand != null &&
                    this.SearchCommand.CanExecute(this.Text))
                {
                    this.SearchCommand.Execute(e);
                }
            };

            this.ShowHideListbox(false);
            this.suggestionsListView.ItemsSource = this.availableSuggestions;
        }

        /// <summary>
        /// Occurs when [selected item changed].
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        /// <summary>
        /// Occurs when [text changed].
        /// </summary>
        public event EventHandler<TextChangedEventArgs> TextChanged;

        /// <summary>
        /// Gets the available Suggestions.
        /// </summary>
        /// <value>The available Suggestions.</value>
        public IEnumerable AvailableSuggestions
        {
            get { return this.availableSuggestions; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [execute on sugestion click].
        /// </summary>
        /// <value><c>true</c> if [execute on sugestion click]; otherwise, <c>false</c>.</value>
        public bool ExecuteOnSuggestionClick
        {
            get { return (bool)this.GetValue(ExecuteOnSuggestionClickProperty); }
            set { this.SetValue(ExecuteOnSuggestionClickProperty, value); }
        }

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>The placeholder.</value>
        public string Placeholder
        {
            get { return (string)this.GetValue(PlaceholderProperty); }
            set { this.SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the search background.
        /// </summary>
        /// <value>The color of the search background.</value>
        public Color SearchBackgroundColor
        {
            get { return (Color)this.GetValue(SearchBackgroundColorProperty); }
            set { this.SetValue(SearchBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search border color.
        /// </summary>
        /// <value>The search border brush.</value>
        public Color SearchBorderColor
        {
            get { return (Color)this.GetValue(SearchBorderColorProperty); }
            set { this.SetValue(SearchBorderColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search border radius.
        /// </summary>
        /// <value>The search border radius.</value>
        public int SearchBorderRadius
        {
            get { return (int)this.GetValue(SearchBorderRadiusProperty); }
            set { this.SetValue(SearchBorderRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the search border.
        /// </summary>
        /// <value>The width of the search border.</value>
        public int SearchBorderWidth
        {
            get { return (int)this.GetValue(SearchBorderWidthProperty); }
            set { this.SetValue(SearchBorderWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public ICommand SearchCommand
        {
            get { return (ICommand)this.GetValue(SearchCommandProperty); }
            set { this.SetValue(SearchCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search horizontal options.
        /// </summary>
        /// <value>The search horizontal options.</value>
        public LayoutOptions SearchHorizontalOptions
        {
            get { return (LayoutOptions)this.GetValue(SearchHorizontalOptionsProperty); }
            set { this.SetValue(SearchHorizontalOptionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText
        {
            get { return (string)this.GetValue(SearchTextProperty); }
            set { this.SetValue(SearchTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the search text button.
        /// </summary>
        /// <value>The color of the search text.</value>
        public Color SearchTextColor
        {
            get { return (Color)this.GetValue(SearchTextColorProperty); }
            set { this.SetValue(SearchTextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search vertical options.
        /// </summary>
        /// <value>The search vertical options.</value>
        public LayoutOptions SearchVerticalOptions
        {
            get { return (LayoutOptions)this.GetValue(SearchVerticalOptionsProperty); }
            set { this.SetValue(SearchVerticalOptionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected command.
        /// </summary>
        /// <value>The selected command.</value>
        public ICommand SelectedCommand
        {
            get { return (ICommand)this.GetValue(SelectedCommandProperty); }
            set { this.SetValue(SelectedCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return (object)this.GetValue(SelectedItemProperty); }
            set { this.SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show search button].
        /// </summary>
        /// <value><c>true</c> if [show search button]; otherwise, <c>false</c>.</value>
        public bool ShowSearchButton
        {
            get { return (bool)this.GetValue(ShowSearchProperty); }
            set { this.SetValue(ShowSearchProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the sugestion background.
        /// </summary>
        /// <value>The color of the sugestion background.</value>
        public Color SuggestionBackgroundColor
        {
            get { return (Color)this.GetValue(SuggestionBackgroundColorProperty); }
            set { this.SetValue(SuggestionBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the suggestion item data template.
        /// </summary>
        /// <value>The sugestion item data template.</value>
        public DataTemplate SuggestionItemDataTemplate
        {
            get { return (DataTemplate)this.GetValue(SuggestionItemDataTemplateProperty); }
            set { this.SetValue(SuggestionItemDataTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Suggestions.
        /// </summary>
        /// <value>The Suggestions.</value>
        public IEnumerable Suggestions
        {
            get { return (IEnumerable)this.GetValue(SuggestionsProperty); }
            set { this.SetValue(SuggestionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of the suggestion.
        /// </summary>
        /// <value>The height of the suggestion.</value>
        public double SuggestionsHeightRequest
        {
            get { return (double)this.GetValue(SuggestionsHeightRequestProperty); }
            set { this.SetValue(SuggestionsHeightRequestProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text background.
        /// </summary>
        /// <value>The color of the text background.</value>
        public Color TextBackgroundColor
        {
            get { return (Color)this.GetValue(TextBackgroundColorProperty); }
            set { this.SetValue(TextBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get { return (Color)this.GetValue(TextColorProperty); }
            set { this.SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text horizontal options.
        /// </summary>
        /// <value>The text horizontal options.</value>
        public LayoutOptions TextHorizontalOptions
        {
            get { return (LayoutOptions)this.GetValue(TextHorizontalOptionsProperty); }
            set { this.SetValue(TextHorizontalOptionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text vertical options.
        /// </summary>
        /// <value>The text vertical options.</value>
        public LayoutOptions TextVerticalOptions
        {
            get { return (LayoutOptions)this.GetValue(TextVerticalOptionsProperty); }
            set { this.SetValue(TextVerticalOptionsProperty, value); }
        }

        /// <summary>
        /// Called when the Placeholder property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnPlaceholderChanged(BindableObject obj, string oldValue, string newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.enteredText.Placeholder = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchBackgroundColor property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchBackgroundColorChanged(BindableObject obj, Color oldValue, Color newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.layout.BackgroundColor = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchBorderColor property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchBorderColorChanged(BindableObject obj, Color oldValue, Color newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.BorderColor = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchBorderRadius property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchBorderRadiusChanged(BindableObject obj, int oldValue, int newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.BorderRadius = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchBorderWidth property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchBorderWidthChanged(BindableObject obj, int oldValue, int newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.BorderWidth = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchHorizontalOptions property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchHorizontalOptionsChanged(BindableObject obj, LayoutOptions oldValue, LayoutOptions newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.HorizontalOptions = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchText property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchTextChanged(BindableObject obj, string oldValue, string newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.Text = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchTextColor property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchTextColorChanged(BindableObject obj, Color oldValue, Color newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.TextColor = newValue;
            }
        }

        /// <summary>
        /// Called when the SearchVerticalOptions property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSearchVerticalOptionsChanged(BindableObject obj, LayoutOptions oldValue, LayoutOptions newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.VerticalOptions = newValue;
            }
        }

        /// <summary>
        /// Called when the ShowSearch property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldShowSearchValue">if set to <c>true</c> [old show search value].</param>
        /// <param name="newShowSearchValue">if set to <c>true</c> [new show search value].</param>
        private static void OnShowSearchChanged(BindableObject obj, bool oldShowSearchValue, bool newShowSearchValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.IsVisible = newShowSearchValue;
            }
        }

        /// <summary>
        /// Called when the SuggestionBackgroundColor property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSuggestionBackgroundColorChanged(BindableObject obj, Color oldValue, Color newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.suggestionsListView.BackgroundColor = newValue;
            }
        }

        /// <summary>
        /// Called when the SuggestionHeightRequest property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSuggestionHeightRequestChanged(BindableObject obj, double oldValue, double newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.suggestionsListView.HeightRequest = newValue;
            }
        }

        /// <summary>
        /// Called when the SuggestionItemDataTemplate property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnSuggestionItemDataTemplateChanged(BindableObject obj, DataTemplate oldValue, DataTemplate newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.suggestionsListView.ItemTemplate = newValue;
            }
        }

        /// <summary>
        /// Texts the vertical options changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnTextVerticalOptionsChanged(BindableObject obj, LayoutOptions oldValue, LayoutOptions newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.enteredText.VerticalOptions = newValue;
            }
        }

        /// <summary>
        /// Called when the TextBackgroundColor property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnTextBackgroundColorChanged(BindableObject obj, Color oldValue, Color newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.enteredText.BackgroundColor = newValue;
            }
        }

        /// <summary>
        /// Called when the TextColor property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnTextColorChanged(BindableObject obj, Color oldValue, Color newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.enteredText.TextColor = newValue;
            }
        }

        /// <summary>
        /// Called when the TextHorizontalOptions property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnTextHorizontalOptionsChanged(BindableObject obj, LayoutOptions oldValue, LayoutOptions newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;
            if (autoCompleteView != null)
            {
                autoCompleteView.enteredText.VerticalOptions = newValue;
            }
        }

        /// <summary>
        /// Called when the Text property has changed.
        /// </summary>
        /// <param name="obj">bindable object</param>
        /// <param name="oldValue">old value</param>
        /// <param name="newValue">new value</param>
        private static void OnTextValueChanged(BindableObject obj, string oldValue, string newValue)
        {
            var autoCompleteView = obj as AutoCompleteView;

            if (autoCompleteView != null)
            {
                autoCompleteView.searchButton.IsEnabled = !string.IsNullOrEmpty(newValue);

                var cleanedNewPlaceHolderValue = Regex.Replace((newValue ?? string.Empty).ToLowerInvariant(), @"\s+", string.Empty);

                if (!string.IsNullOrEmpty(cleanedNewPlaceHolderValue) &&
                    autoCompleteView.Suggestions != null)
                {
                    var filteredSuggestions = autoCompleteView.Suggestions.Cast<object>()
                        .Where(x => Regex.Replace(
                            x.ToString().ToLowerInvariant(), @"\s+", string.Empty).Contains(cleanedNewPlaceHolderValue))
                        .OrderByDescending(x => Regex.Replace(
                            x.ToString().ToLowerInvariant(), @"\s+", string.Empty)
                        .StartsWith(cleanedNewPlaceHolderValue)).ToList();

                    autoCompleteView.availableSuggestions.Clear();
                    if (filteredSuggestions.Count > 0)
                    {
                        foreach (var suggestion in filteredSuggestions)
                        {
                            autoCompleteView.availableSuggestions.Add(suggestion);
                        }

                        autoCompleteView.ShowHideListbox(true);
                    }
                    else
                    {
                        autoCompleteView.ShowHideListbox(false);
                    }
                }
                else
                {
                    if (autoCompleteView.availableSuggestions.Count > 0)
                    {
                        autoCompleteView.availableSuggestions.Clear();
                        autoCompleteView.ShowHideListbox(false);
                    }
                }
            }
        }

        /// <summary>
        /// Called when [selected item changed].
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        private void OnSelectedItemChanged(object selectedItem)
        {
            this.SelectedItem = selectedItem;

            if (this.SelectedCommand != null)
            {
                this.SelectedCommand.Execute(selectedItem);
            }

            var handler = this.SelectedItemChanged;
            if (handler != null)
            {
                handler(this, new SelectedItemChangedEventArgs(selectedItem));
            }
        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void OnTextChanged(TextChangedEventArgs e)
        {
            var handler = this.TextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Shows the hide listbox.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void ShowHideListbox(bool show)
        {
            this.suggestionsListView.IsVisible = show;
        }
    }
}
