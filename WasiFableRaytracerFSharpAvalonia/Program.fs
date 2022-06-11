namespace WasiFableRaytracerFSharpAvalonia

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts


type MainWindow() as this =
    inherit HostWindow()
    
    do
        base.Title <- "AvaloniaApp"
        base.Width <- 512.0
        base.Height <- 512.0
        this.Content <- Counter.view (int base.Width) (int base.Height)

        //this.VisualRoot.VisualRoot.Renderer.DrawFps <- true
        //this.VisualRoot.VisualRoot.Renderer.DrawDirtyRects <- true

        
type App() =
    inherit Application()
        
    override this.Initialize() =
        this.Styles.Add (FluentTheme(baseUri = null, Mode = FluentThemeMode.Light))

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

module Program =

    [<EntryPoint>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)