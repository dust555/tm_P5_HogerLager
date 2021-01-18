using System.Windows;
using System;

namespace HogerLager
{

    public enum FaceEnum{
        RUITEN,
        KLAVEREN,
        SCHOPPEN,
        HARTEN

    }


    public class Card:IComparable
    {

        Random rand = new Random();

        public int Number { get; set; }
        public FaceEnum Face { get; set; }

        public Card(){
            Number = rand.Next(1,14);
            Face = (FaceEnum)rand.Next(0,4);
        }

        public override string ToString(){
            return FaceEnum.GetName(Face)+" "+Number;
        }

        public int CompareTo(object obj){
            Card c = (Card)obj;

            if(c.Number < this.Number){
                return 1;
            }
            else if(c.Number > this.Number){
                return -1;
            }
            else if(c.Face < this.Face){
                return 1;
            }
            else if(c.Face > this.Face){
                return -1;
            }
            else{
                return 0;
            }
        }


    }
}