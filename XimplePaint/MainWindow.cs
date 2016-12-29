using System;
using Gtk;
using Cairo;

public partial class MainWindow: Gtk.Window
{
    
    public bool changeColor = false;

    public bool buttonPress;

    public int xDrawingArea, yDrawingArea;

    public int xClickedArea, yClickedArea;

    public int xLastDrawingArea, yLastDrawingArea;


    public Antialias antiAlias;

    private Random random;

    private Color colorPaint;

    public MainWindow()
        : base(Gtk.WindowType.Toplevel)
    {
        Build();
        fixedPaintWindow.ModifyBg(StateType.Normal, new Gdk.Color(123, 0, 0));
        this.drawingAreaPaint.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));

        buttonPress = false;
        xDrawingArea = 300;
        yDrawingArea = 300;

        antiAlias = Antialias.Gray;

        random = new Random();

        colorPaint = new Color();

        colorButtonPicker.Color = new Gdk.Color(0, 0, 0);

        colorButtonPicker.Hide();

        //colorPaint.
        //xClickedArea

    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    // Convert Gdk Color to Cairo Color (255 -> 1.0)
    public Cairo.Color ToCairoColor (Gdk.Color color)
    {
        return new Cairo.Color ((double)color.Red / ushort.MaxValue,
            (double)color.Green / ushort.MaxValue, (double)color.Blue /
            ushort.MaxValue);
    }

    // Paints the line when mouse button is pressed on drawing area and mouse is moved
    public void NewCirclePaint(int x, int y)
    {
        
        Cairo.Context cairo = Gdk.CairoHelper.Create(drawingAreaPaint.GdkWindow);

        cairo.Antialias = Antialias.Gray;
        cairo.LineWidth = spinbuttonSize.Value;
        if (toggleButtonPaint.Active == true)
        {
            cairo.SetSourceColor(ToCairoColor(colorButtonPicker.Color));
        }
        else if(toggleButtonErase.Active == true)
        {
            cairo.SetSourceRGB(255, 255, 255);
        }
        cairo.LineCap = LineCap.Round;
        cairo.MoveTo ((int)xLastDrawingArea, (int)yLastDrawingArea);
        cairo.LineTo ((int)xDrawingArea, (int)yDrawingArea);
        cairo.Stroke();

        cairo.GetTarget ().Dispose ();
        cairo.Dispose();

    }

    protected void OnDrawingAreaPaintMotionNotifyEvent(object o, MotionNotifyEventArgs args)
    {
        xLastDrawingArea = xDrawingArea;
        yLastDrawingArea = yDrawingArea;

        xDrawingArea = (int)args.Event.X;
        yDrawingArea = (int)args.Event.Y;

        if (buttonPress == true && toggleButtonPaint.Active == true || buttonPress == true && toggleButtonErase.Active == true)
        {
            NewCirclePaint((int)args.Event.X, (int)args.Event.Y);
        }
    }

    protected void OnDrawingAreaPaintButtonPressEvent(object o, ButtonPressEventArgs args)
    {
        buttonPress = true;

        xClickedArea = xDrawingArea;
        yClickedArea = yDrawingArea;

        if (toggleButtonPaint.Active == true || toggleButtonErase.Active == true)
        {
            NewCirclePaint((int)args.Event.X, (int)args.Event.Y);
        }
    }

    protected void OnDrawingAreaPaintButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
    {
        buttonPress = false;
    }

    protected void OnToggleButtonPaintPressed(object sender, EventArgs e)
    {
        //labelSize.ModifyFg(StateType.Normal, new Gdk.Color(255, 0, 0));
        toggleButtonErase.Active = false;
    }

    protected void OnToggleButtonErasePressed(object sender, EventArgs e)
    {
        toggleButtonPaint.Active = false;

    }

    protected void OnToggleButtonPaintToggled(object sender, EventArgs e)
    {
        if (toggleButtonErase.Active == false)
        {
            colorButtonPicker.Show();
        }
        if (toggleButtonPaint.Active == false)
        {
            colorButtonPicker.Hide();
        }
    }

    protected void OnToggleButtonEraseToggled(object sender, EventArgs e)
    {
        if (toggleButtonPaint.Active == false)
        {
            colorButtonPicker.Hide();
        }
    }
    protected void OnClearAllPaintActionActivated(object sender, EventArgs e)
    {
        drawingAreaPaint.GdkWindow.Clear();
    }

    public void SaveImageToJpg()
    {

        ImageSurface draw = new ImageSurface(Format.Argb32, drawingAreaPaint.Allocation.Width, drawingAreaPaint.Allocation.Height);

        Cairo.Context context = Gdk.CairoHelper.Create (drawingAreaPaint.GdkWindow);

        using (Cairo.Context cr = Gdk.CairoHelper.Create(drawingAreaPaint.GdkWindow))
        {
            // draw the image with Cairo
            //cr.
        }

        //Gdk.Pixbuf pixbuf = Gdk.Pixbuf.FromDrawable(drawingAreaPaint.GdkWindow, Gdk.Colormap.System);

        //drawingAreaPaint.GdkWindow

        //draw.WriteToPng("savepicture.png");
    }
}
