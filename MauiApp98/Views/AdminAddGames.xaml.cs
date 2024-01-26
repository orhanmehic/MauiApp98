using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Services;

namespace MauiApp98.Views;

public partial class AdminAddGames : ContentPage
{

    private GameService gameService;
    public ObservableCollection<Games> Games { get; set; }

    public AdminAddGames()
    {
        InitializeComponent();
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        gameService = new GameService(database);
        Games = new ObservableCollection<Games>(gameService.getAllGames());
        BindingContext = this;
    }
}