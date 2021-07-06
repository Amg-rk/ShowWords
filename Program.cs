using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
//using System.Threading.Tasks;

namespace WordControl
{
    class StringControler
    {
        //自動プロパティ
        public string FilePass {get; set;}

        public int FileSize {get; set;}

        public string Text {get; set;}

         public int Time {get; set;}
        
        //ファイルパス名を引数として受け取り、string型で返すメソッド
        public char[] MakeArray(){
           
            //streamから1バイトごとにデータを読み込みbytes配列に格納する。
            using(FileStream stream = File.Open(this.FilePass,FileMode.Open)){
                this.FileSize = (int)stream.Length;
                byte[] bytes = new byte[FileSize];
                stream.Read(bytes,0,FileSize);

                //byte型配列を文字列に変換し、char型配列に直す。
                this.Text = System.Text.Encoding.UTF8.GetString(bytes);
                char[] wordArray = Text.ToCharArray();
                return wordArray;
            }
        }
        //char型の配列を引数としてstring型に変換し、小文字や句読点を直前の要素に結合したリストを返す。
        //正規表現で特殊記号なども扱いたい
        public List<string> MakeList(char[] charArray){
            var wordList = new List<string>{};
            var halfStrList = new List<string>{
                "、","。","ぁ","ぃ","ぅ","ぇ","ぉ","っ","ァ","ィ","ゥ","ェ","ォ","ッ","ゃ","ゅ","ょ","ャ","ュ","ョ","ｧ","ｨ","ｩ","ｪ","ｫ","ｬ","ｭ","ｮ","ｯ","!","?","！","？"
            };
            foreach(var c in charArray){
                if(halfStrList.Contains(c.ToString())){
                    wordList.Add(wordList[wordList.Count-1]+(c.ToString()));
                    wordList.Remove(wordList[wordList.Count-2]);
                }
                else{
                    wordList.Add((c.ToString()));
                    }
            }
            return wordList;
        }

        //string型のリストと一文字ごとの時間(ミリ秒)を受け取り、一定時間ごとに表示する。
        public  void ShowString(List<string> stringList){
            foreach(var str in stringList){
                Console.Write(str);
                //async修飾子をつけて await Task.Delay(this.Time);　とするとフリーズではなく非同期処理が可能。
                Thread.Sleep(this.Time);
            }
          
        }
       
    }
    class Program
    {
         static void Main(string[] args)
        {
            var s = new StringControler{
            FilePass = "/WordControl/表示する文字列.txt",
            Time = 40
            };
            //Console.WriteLine(string.Join(",",s.MakeArray()));
            s.ShowString(s.MakeList(s.MakeArray()));
        }
    }
}
