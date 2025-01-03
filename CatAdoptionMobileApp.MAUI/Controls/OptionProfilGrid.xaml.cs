namespace CatAdoptionMobileApp.MAUI.Controls;

public partial class OptionProfilGrid : ContentView
{

    public static readonly BindableProperty TextProperty
        = BindableProperty.Create(nameof(Text), typeof(string), typeof(OptionProfilGrid), string.Empty);

    public static readonly BindableProperty ImageSourceProperty
        = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(OptionProfilGrid), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public OptionProfilGrid()
	{
		InitializeComponent();
	}

    public event EventHandler<string> Tapped;
    private void OnTappedHandler(object sender, TappedEventArgs e)
    {
        Tapped?.Invoke(this, Text);
    }
}