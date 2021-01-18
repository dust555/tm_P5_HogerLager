using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Net.Http;

namespace HogerLager
{

    public delegate void MessageBoxDelegate(String message);

    public class Game:INotifyPropertyChanged
    {

        public event MessageBoxDelegate MessageBoxEvent;


        Card NextCard;
        Card _currentCard;

        public Card CurrentCard { 
            get{return _currentCard;} 
            set{_currentCard = value; OnPropertyChanged();} 
        }

        Boolean _buttonEnabled;
        public Boolean ButtonEnabled { 
            get{return _buttonEnabled;} 
            set{_buttonEnabled = value; OnPropertyChanged();} 
        }

        Boolean _uploadButtonEnabled;
        public Boolean UploadButtonEnabled { 
            get{return _uploadButtonEnabled;} 
            set{_uploadButtonEnabled = value; OnPropertyChanged();} 
        }

        int _score;
        public int Score { 
            get{return _score;} 
            set{_score = value; OnPropertyChanged();} 
        }


        public List<Card> CardList { get; set; }

        String _cardListString;
        public String CardListString {
            get{return _cardListString;} 
            set{_cardListString = value; OnPropertyChanged();} 
         }

        public Game(){
            CardList = new List<Card>();
            Score = 0;
            NextCard = new Card();
            CurrentCard = new Card();
            ButtonEnabled = true;
            UploadButtonEnabled = true;
        }

        public void NewGame(object sender, RoutedEventArgs args){
            CardList.Clear();
            Score = 0;
            NextCard = new Card();
            CurrentCard = new Card();
            ButtonEnabled = true;
            
            CardListString = ToString();
        }

        public void Guess(object sender, RoutedEventArgs args){
            Button b = (Button)sender;

            CardList.Add(CurrentCard);
            CardListString = ToString();

            if((b.Name.Equals("HigherButton") && CurrentCard.CompareTo(NextCard) < 0) ||
                (b.Name.Equals("LowerButton") && CurrentCard.CompareTo(NextCard) > 0)){
                    Score += 1;
                    CurrentCard = NextCard;
                    NextCard = new Card();
            }
            else{
                ButtonEnabled = false;
                MessageBoxEvent?.Invoke("Game Over!");
            }
        }


        public void SortCardList(object sender, RoutedEventArgs args){
            Button b = (Button)sender;

            CardList.Sort();
            if(b.Name.Equals("SortCardListDESC")){
                CardList.Reverse();
            }
            CardListString = ToString();
        }

        public void UploadScore(object sender, RoutedEventArgs args){
            UploadButtonEnabled = false;
            Task.Run(UploadScoreAsync);
        }

        async Task UploadScoreAsync(){
            HttpClient client = new HttpClient();

            String response = await client.GetStringAsync("http://www.seamine.be/cnet.php?score="+Score);

            if(response.Equals("1")){
                MessageBoxEvent?.Invoke("Upload OK");
            }
            else{
                MessageBoxEvent?.Invoke("Upload niet OK");
            }
            UploadButtonEnabled = true;

        }

        new public String ToString(){
            var str = "";
            foreach(Card c in CardList){
                str += " - "+c.ToString();
            }
            return str;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}