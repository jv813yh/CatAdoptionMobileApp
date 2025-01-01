namespace CatAdoptionMobileApp.MAUI.Controls;

public partial class CatFeatControl : ContentPage
{
	public CatFeatControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(string), typeof(CatFeatControl), string.Empty);

	public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(CatFeatControl), string.Empty);

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(string), typeof(CatFeatControl), string.Empty);

    public string Icon 
	{
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
	public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);  
    }
    public string Value
    {
        get => (string)GetValue(ValueProperty); 
        set => SetValue(ValueProperty, value);
    }
}