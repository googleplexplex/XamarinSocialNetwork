using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using XamarinNetworkProj.Model;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using XamarinNetworkProj.Views;

namespace XamarinNetworkProj
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "network.db";
        public static FriendAsyncRepository friendsTable;
        public static SQLiteAsyncConnection database;
        public static FriendAsyncRepository FriendsTable
        {
            get
            {
                if (friendsTable == null)
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
                    if(database == null)
                        database = new SQLiteAsyncConnection(dbPath);
                    friendsTable = new FriendAsyncRepository(database);
                }
                return friendsTable;
            }
        }
        public static PostAsyncRepository postsTable;
        public static PostAsyncRepository PostsTable
        {
            get
            {
                if (postsTable == null)
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
                    if (database == null)
                        database = new SQLiteAsyncConnection(dbPath);
                    postsTable = new PostAsyncRepository(database);
                }
                return postsTable;
            }
        }

        static public Account AccountConstr(string nickname, string password, string desc, int id = 0)
        {
            Account a = new Account();
            a.Id = id;
            a.nickname = nickname;
            a.password = password;
            a.desc = desc;
            a.likedPosts = JsonConvert.SerializeObject(new List<int>());
            return a;
        }
        public Post PostConstr(int autorId, string content, int likes, int id = 0)
        {
            Post a = new Post();
            a.Id = id;
            a.autorId = autorId;
            a.content = content;
            a.likes = likes;
            return a;
        }

        public App()
        {
            InitializeComponent();
        }

        protected async override void OnStart()
        {
            //await App.FriendsTable.CreateTable();

            ////await App.FriendsTable.Clear();
            //int err = await App.FriendsTable.InsertItemAsync(AccountConstr("LunarDreamer", "123456", "Dance in the moonlight and dream with the stars.", 1));
            //App.FriendsTable.InsertItemAsync(AccountConstr("MysticSoul ", "123456", "Embrace the mysteries of the universe within your soul.", 2));
            //App.FriendsTable.InsertItemAsync(AccountConstr("EternalExplorer", "123456", "Venture into the depths of the unknown and uncover eternal truths.", 3));
            //App.FriendsTable.InsertItemAsync(AccountConstr("ShadowSeeker", "123456", "Embrace the darkness within to find the light that guides your path.", 4));
            //App.FriendsTable.InsertItemAsync(AccountConstr("Eternal", "123456", "Venture", 5));
            //App.FriendsTable.InsertItemAsync(AccountConstr("Shadow", "123456", "Embrace", 6));
            //App.FriendsTable.InsertItemAsync(AccountConstr("SoulfulSeeker", "123456", "Seek the depths of your soul to discover the true essence of your being.", 7));
            //App.FriendsTable.InsertItemAsync(AccountConstr("StarryDreamer", "123456", "Dream under the starlit sky and let the universe whisper its secrets to your soul.", 8));

            //List<Account> a = App.FriendsTable.database.Table<Account>().OrderBy(f => f.Id).ToListAsync().Result;
            //int minId = a[0].Id;
            //App.PostsTable.Clear();
            //App.PostsTable.InsertItemAsync(PostConstr(minId, "hi im 1", 0));
            //App.PostsTable.InsertItemAsync(PostConstr(minId + 1, "hi im 2", 0));
            //App.PostsTable.InsertItemAsync(PostConstr(minId + 2, "hi im 3", 0));

            ////await App.PostsTable.Clear();
            //int postid = 1;
            //await App.PostsTable.InsertItemAsync(PostConstr(postid++, "The only way to do great work is to love what you do.", 0, postid++));
            //await App.PostsTable.InsertItemAsync(PostConstr(postid++, "In the end, it's not the years in your life that count. It's the life in your years.", 0, postid++));
            //await App.PostsTable.InsertItemAsync(PostConstr(postid++, "You miss 100% of the shots you don't take.", 0, postid++));
            //await App.PostsTable.InsertItemAsync(PostConstr(postid++, "The only thing we have to fear is fear itself.", 0, postid++));
            //await App.PostsTable.InsertItemAsync(PostConstr(postid++, "Success is not the key to happiness. Happiness is the key to success.", 0, postid++));


            //await App.FriendsTable.DeleteItemsAsyncById(10);


            if (!App.Current.Properties.ContainsKey("user"))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
