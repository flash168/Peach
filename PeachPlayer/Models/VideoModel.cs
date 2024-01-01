using System.Collections.ObjectModel;

namespace PeachPlayer.Models
{
    public class VideoListModel : NotifyProperty
    {
        public VideoListModel() { }
        public int limit { get; set; }
        public int page { get; set; }
        public int pagecount { get; set; }
        public int total { get; set; }


        private ObservableCollection<VideoModel> list;
        public ObservableCollection<VideoModel> List
        {
            get => list;
            set => SetProperty(ref list, value);
        }

    }

    public class VideoModel : NotifyProperty
    {
        /// <summary>
        /// 
        /// </summary>
        private string vod_id;
        public string Vod_id
        {
            get => vod_id;
            set => SetProperty(ref vod_id, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public int type_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type_id_1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int group_id { get; set; }
        /// <summary>
        /// 鬼灭之刃 无限列车篇 鬼滅の刃 無限列車編
        /// </summary>
        private string vod_name;
        public string Vod_name
        {
            get => vod_name;
            set => SetProperty(ref vod_name, value);
        }

        /// <summary>
        /// 鬼灭之刃 无限列车篇TV版
        /// </summary>
        public string Vod_sub { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_en { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_letter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string vod_color;
        public string Vod_color
        {
            get => vod_color;
            set => SetProperty(ref vod_color, value);
        }
        /// <summary>
        /// 鬼灭之刃,动漫,日本,日本动画,炎柱,动画,2021,喜欢
        /// </summary>
        public string Vod_tag { get; set; }
        /// <summary>
        /// 动画,奇幻,动漫
        /// </summary>
        private string vod_class;
        public string Vod_class { get => vod_class; set => SetProperty(ref vod_class, value); }
        /// <summary>
        /// 
        /// </summary>
        private string vod_pic;
        public string Vod_pic { get => vod_pic; set => SetProperty(ref vod_pic, value); }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pic_thumb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pic_slide { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pic_screenshot { get; set; }
        /// <summary>
        /// 花江夏树,鬼头明里,下野纮,松冈祯丞,日野聪,平川大辅
        /// </summary>
        private string vod_actor;
        public string Vod_actor { get => vod_actor; set => SetProperty(ref vod_actor, value); }
        /// <summary>
        /// 外崎春雄
        /// </summary>
        public string Vod_director { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_writer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_behind { get; set; }
        /// <summary>
        /// 2019年播出的TV动画的续篇，讲述灶门炭治郎和炼狱杏寿郎与下弦之壹魇梦作战的故事.
        /// </summary>
        private string vod_blurb;
        public string Vod_blurb { get => vod_blurb; set => SetProperty(ref vod_blurb, value); }
        /// <summary>
        /// 更新至2话
        /// </summary>
        private string vod_remarks;
        public string Vod_remarks { get => vod_remarks; set => SetProperty(ref vod_remarks, value); }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pubdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_serial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_tv { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_weekday { get; set; }
        /// <summary>
        /// 日本
        /// </summary>
        private string vod_area;
        public string Vod_area { get => vod_area; set => SetProperty(ref vod_area, value); }
        /// <summary>
        /// 日语
        /// </summary>
        public string Vod_lang { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string vod_year;
        public string Vod_year { get => vod_year; set => SetProperty(ref vod_year, value); }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_version { get; set; }
        /// <summary>
        /// 正片
        /// </summary>
        public string Vod_state { get; set; }
        /// <summary>
        /// 豆瓣
        /// </summary>
        public string Vod_author { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_jumpurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_tpl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_tpl_play { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_tpl_down { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_isend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_lock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_copyright { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_points { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_points_play { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_points_down { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_hits { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_hits_day { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_hits_week { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_hits_month { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_up { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_down { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string vod_score;
        public string Vod_score { get => vod_score; set => SetProperty(ref vod_score, value); }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_score_all { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_score_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_time_add { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_time_hits { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_time_make { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_trysee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_douban_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_douban_score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_reurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_rel_vod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_rel_art { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pwd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pwd_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pwd_play { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pwd_play_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pwd_down { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_pwd_down_url { get; set; }
        /// <summary>
        /// 2019年播出的TV动画的续篇，讲述灶门炭治郎和炼狱杏寿郎与下弦之壹魇梦作战的故事.
        /// </summary>
        private string vod_content;
        public string Vod_content { get => vod_content; set => SetProperty(ref vod_content, value); }
        /// <summary>
        /// 
        /// </summary>
        private string vod_play_from;
        public string Vod_play_from
        {
            get { return vod_play_from; }
            set
            {
                vod_play_from = value;
                string[] plays = value.Replace("$$$", "|").Split('|');
                if ((vod_play_urls == null || vod_play_urls.Count < 0) && plays.Length > 0)
                {
                    vod_play_urls = new ObservableCollection<PlayerData>();
                    for (int i = 0; i < plays.Length; i++)
                    {
                        vod_play_urls.Add(new PlayerData(plays[i]));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Vod_play_server { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_play_note { get; set; }
        /// <summary>
        /// 第01集$https://uujs.ml/api/v3/file/source/2945/%E9%AC%BC%E7%81%AD%E4%B9%8B%E5%88%83S02E01.mp4?sign=o3ssyUj9wATk30APi-WBd7iQgdt-0JjUgunfwxOtiZE%3D%3A0#第02集$https://uujs.ml/api/v3/file/source/3074/S02E02.mp4?sign=PMw1mVd68ZZ_xqvMnMzAJNhfBZMMXMuE6aHebgDuMgE%3D%3A0
        /// </summary>
        public string Vod_play_url { get; set; }


        private ObservableCollection<PlayerData> vod_play_urls;
        public ObservableCollection<PlayerData> Vod_play_urls
        {
            get
            {
                if (!string.IsNullOrEmpty(Vod_play_url))
                {
                    if (vod_play_urls?.Count <= 0) vod_play_urls = new ObservableCollection<PlayerData>();

                    var jishu = new ObservableCollection<JiShuInfo>();

                    string[] plays = Vod_play_url.Replace("$$$", "|").Split('|');
                    for (int i = 0; i < plays.Length; i++)
                    {
                        //if (!plays[i].Contains("#")) continue;
                        string[] jis = plays[i].Split('#');
                        jishu.Clear();
                        for (int j = 0; j < jis.Length; j++)
                        {
                            if (!jis[j].Contains("$")) continue;
                            string[] ji = jis[j].Split('$');
                            if (ji.Length >= 2)
                                jishu.Add(new JiShuInfo(ji[0], ji[1]));
                        }
                        if (vod_play_urls?.Count <= 0)
                            vod_play_urls.Add(new PlayerData(jishu));
                        else
                            vod_play_urls[i].Collection = jishu;
                    }
                }
                return vod_play_urls;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Vod_down_from { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_down_server { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_down_note { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_down_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Vod_plot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_plot_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Vod_plot_detail { get; set; }
        /// <summary>
        /// 动漫
        /// </summary>
        private string type_name;
        public string Type_name { get => type_name; set => SetProperty(ref type_name, value); }


    }


    public class PlayerData
    {
        public PlayerData()
        {

        }
        public PlayerData(string title)
        {
            Title = title;
        }
        public PlayerData(ObservableCollection<JiShuInfo> collection)
        {
            Collection = collection;
        }
        public PlayerData(string title, ObservableCollection<JiShuInfo> collection)
        {
            Title = title;
            Collection = collection;
        }
        public string Title { get; set; }
        public ObservableCollection<JiShuInfo> Collection { get; set; }

    }

    public class JiShuInfo
    {
        public JiShuInfo(string name, string url)
        {
            Name = name;
            Purl = url;
        }
        public string Name { get; set; }
        public string Purl { get; set; }

    }





}
