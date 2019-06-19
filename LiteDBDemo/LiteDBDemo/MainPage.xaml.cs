using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using LiteDBDemo.Helpers;
using LiteDBDemo.Models;
using Xamarin.Forms;

namespace LiteDBDemo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        LiteDatabase _dataBase;

        public MainPage()
        {
            InitializeComponent();

            _dataBase = new LiteDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MeuBanco.db"));
        }

        private Stream GetImageStreamFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    Stream stream = new MemoryStream(imageBytes);
                    return stream;
                }
            }
            return null;
        }

        public void CriaBot_Clicked(object sender, EventArgs e)
        {
            LiteCollection<Bot> Bots = _dataBase.GetCollection<Bot>();

            int idBot = Bots.Count() + 1;
            Bot porco = new Bot
            {
                BotId = idBot,
                Nome = $"Bot {idBot}",
            };

            Bots.Upsert(porco);

            lbStatusBot.Text = "Bot Criado!";
        }

        public void ModificaBot_Clicked(object sender, EventArgs e)
        {
            LiteCollection<Bot> Bots = _dataBase.GetCollection<Bot>();

            if (Bots.Count() > 0)
            {
                var bot = Bots.FindAll().FirstOrDefault();
                bot.Nome = "Renato Groffe";
                Bots.Upsert(bot);

                Stream stream = GetImageStreamFromUrl("http://azureweekexperience.azurewebsites.net/wp-content/uploads/2017/06/Renato-Grofe.jpg");
                if (stream != null)
                {
                    //Verfica se ja existe a imagem,se existir apaga
                    if (_dataBase.FileStorage.Exists(bot.BotId.ToString()))
                    {
                        _dataBase.FileStorage.Delete(bot.BotId.ToString());
                    }
                    _dataBase.FileStorage.Upload(bot.BotId.ToString(), "Teste", stream);

                }

                lbStatusBot.Text = "Bot modificado";
            }

        }

        public void RetornaBot_Clicked(object sender, EventArgs e)
        {
            LiteCollection<Bot> Bots = _dataBase.GetCollection<Bot>();

            if (Bots.Count() > 0)
            {
                var bot = Bots.FindAll().FirstOrDefault();
                imgBot.Source = ImageSource.FromStream(() => _dataBase.FileStorage.FindById(bot.BotId.ToString()).OpenRead());
                lbStatusBot.Text = "Bot Retornado";
            }
        }

       
    }
}
