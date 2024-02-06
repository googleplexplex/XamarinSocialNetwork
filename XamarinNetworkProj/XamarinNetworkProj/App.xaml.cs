using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using XamarinNetworkProj.Model;

namespace XamarinNetworkProj
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "network.db";
        public static FriendAsyncRepository database;
        public static FriendAsyncRepository Database
        {
            get
            {
                if (database == null)
                {
                    // путь, по которому будет находиться база данных
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
                    // если база данных не существует (еще не скопирована)
                    if (!File.Exists(dbPath))
                    {
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"XamarinNetworkProj.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                fs.Flush();
                            }
                        }
                    }
                    database = new FriendAsyncRepository(dbPath);
                }
                return database;
            }
        }

        private Account AccountConstr(string nickname, string phone, string desc, int id = 0)
        {
            Account a = new Account();
            a.Id = id;
            a.nickname = nickname;
            a.phone = phone;
            a.desc = desc;
            return a;
        }
        public App()
        {
            InitializeComponent();

            App.Database.Clear();
            App.Database.SaveItemAsync(AccountConstr("LunarDreamer", "123", "Dance in the moonlight and dream with the stars."));
            App.Database.SaveItemAsync(AccountConstr("MysticSoul ", "123", "Embrace the mysteries of the universe within your soul."));
            App.Database.SaveItemAsync(AccountConstr("EternalExplorer", "456", "Venture into the depths of the unknown and uncover eternal truths."));
            App.Database.SaveItemAsync(AccountConstr("ShadowSeeker", "789", "Embrace the darkness within to find the light that guides your path."));
            App.Database.SaveItemAsync(AccountConstr("EternalExplorer", "456", "Venture into the depths of the unknown and uncover eternal truths."));
            App.Database.SaveItemAsync(AccountConstr("ShadowSeeker", "789", "Embrace the darkness within to find the light that guides your path."));
            App.Database.SaveItemAsync(AccountConstr("SoulfulSeeker", "369", "Seek the depths of your soul to discover the true essence of your being."));
            App.Database.SaveItemAsync(AccountConstr("StarryDreamer", "579", "Dream under the starlit sky and let the universe whisper its secrets to your soul."));

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
