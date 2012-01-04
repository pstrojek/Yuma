using System;
using Data.Realm;
using Data;
using AIMLbot;
using System.Text;
using System.Linq;
using System.Collections.Generic;




namespace CsClient
{
    public class Program
    {

        static AgentAPI agentTomek; //nasz agent, instancja klasy AgentAPI
        static int energy; //tu zapisujemy aktualną energię naszego agenta
        static WorldParameters cennikSwiata; //tu zapisujemy informacje o świecie
        static int[,] Przeszkody; //deklaracja tablicy, w ktorej bedziemy przetrzymywac wsp. przeszkod.
        static int[,] ZrodlaNieOdn; //deklaracja tablicy, w ktorej bedziemy trzymac wsp. zrodel energii NieOdnawialnych
        static int[,] ZrodlaOdn; //tutaj trzymamy wsp. odnawialnych
        static int ObecnePolozenieX=0;
        static int ObecnePolozenieY=0;
        static string kierunek="N"; // jako domyslna wart. przyjmuje N, nie jest wazne jaki kierunek jest to faktycznie
         // bo wykorzystujemy to tylko do wlasnych potrzeb.
         // pozniej przyjmuje W, N, E, S - potrzebne od zapamietywania kierunku -> modyfikacji polozenia
        int poleDoceloweX=0;
        int poleDoceloweY=0;
        
        //public Bot myBot;
       	//public User myUser;

        // Nasza metoda nasłuchująca
        static void Listen(String krzyczacyAgent, String komunikat) {
            Console.WriteLine(krzyczacyAgent + " krzyczy " + komunikat);
        }

        static void Main(string[] args)
        {
            //powtarzamy czynnosci az nam się uda
            while (true)
            {


                agentTomek = new AgentAPI(Listen); //tworzymy nowe AgentAPI, podając w parametrze naszą metodę nasłuchującą

                // pobieramy parametry połączenia i agenta z klawiatury
                //Console.Write("Podaj IP serwera: ");
                String ip = "atlantyda.vm.wmi.amu.edu.pl"; //Console.ReadLine();

                //Console.Write("Podaj nazwe druzyny: ");
                String groupname = "Yuma"; //Console.ReadLine();

                //Console.Write("Podaj haslo: ");
                String grouppass = "odarjd"; //Console.ReadLine();

                //Console.Write("Podaj nazwe swiata: ");
                String worldname = "Yuma"; //Console.ReadLine();

                Console.Write("Podaj imie: ");
                String imie = Console.ReadLine();

                try
                {
                    //łączymy się z serwerem. Odbieramy parametry świata i wyświetlamy je
                    cennikSwiata = agentTomek.Connect(ip, 6008, groupname, grouppass, worldname, imie);
                    Console.WriteLine(cennikSwiata.initialEnergy + " - Maksymalna energia");
                    Console.WriteLine(cennikSwiata.maxRecharge + " - Maksymalne doładowanie");
                    Console.WriteLine(cennikSwiata.sightScope + " - Zasięg widzenia");
                    Console.WriteLine(cennikSwiata.hearScope + " - Zasięg słyszenia");
                    Console.WriteLine(cennikSwiata.moveCost + " - Koszt chodzenia");
                    Console.WriteLine(cennikSwiata.rotateCost + " - Koszt obrotu");
                    Console.WriteLine(cennikSwiata.speakCost + " - Koszt mówienia");

                    //ustawiamy nasza energie na poczatkowa energie kazdego agenta w danym swiecie
                    energy = cennikSwiata.initialEnergy;
                    //przechodzimy do obslugi zdarzen z klawiatury. Zamiast tej funkcji wstaw logikę poruszania się twojego agenta.
                    KeyReader();
                    //na koncu rozlaczamy naszego agenta
                    agentTomek.Disconnect();
                    Console.ReadKey();
                    break;
                }
                //w przypadku mało poważnego błędu, jak podanie złego hasła, rzucany jest wyjątek NonCriticalException; zaczynamy od nowa
                catch (NonCriticalException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // w przypadku każdego innego wyjątku niż NonCriticalException powinniśmy zakończyć program; taki wyjątek nie powinien się zdarzyć
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ReadKey();
                }
            }
        }

        //funkcja wykonywująca określone akcję w zależności od naciśniętego przycisku
        static void KeyReader() {
            bool loop = true;
            while(loop) {
                Console.WriteLine("Moja energia: " + energy);
                switch(Console.ReadKey().Key) {
                		case ConsoleKey.Spacebar: Testowe();
                        break;
                    case ConsoleKey.R: AIML();
                        break;
                    case ConsoleKey.UpArrow: StepForward();
                        break;
                    case ConsoleKey.LeftArrow: RotateLeft();
                        break;
                    case ConsoleKey.RightArrow: RotateRight();
                        break;
                    case ConsoleKey.Enter: Speak();
                        break;
                    case ConsoleKey.Q: loop = false;
                        break;
                    case ConsoleKey.D: agentTomek.Disconnect();
                        break;
                    default: Console.Beep();
                        break;
                }
            }
        }
        
        
        
        static void AIML() {
        	
        	        	
        	getOutput("GDZIE JEST ZRODLO ENERGI");
        }
        
        
   
        
       static void  getOutput(String input)
	 {
       	
       	
       	
        	Bot myBot = new Bot();
			User myUser = new User("consoleUser", myBot);
			
				 myBot.loadSettings();
	 myBot.isAcceptingUserInput = false;
	 myBot.loadAIMLFromFiles();
	 myBot.isAcceptingUserInput = true;
        	
        	
        	
	 Request r = new Request(input, myUser, myBot);
	 Result res = myBot.Chat(r);
	 Console.WriteLine("Bot: " + res.Output);
	 }



static void Testowe() {

        		Look();
        		for(int i=0 ; i <  200 ; i++) {
        		StepForward();
        		
        		if ( i == 38)
        			RotateRight();
        		else if ( i == 79)
        			RotateLeft();
        		else if ( i == 134)
        			RotateRight();
        		else if ( i == 166)
        			RotateLeft();
        			
        	        		        			       			
        		}

}
        
        static void OminPrzeszkode(){
        	if (!agentTomek.StepForward()){
        		int right = 0;
        		int left = 0;
        		
        		if (right<left){
        			RotateRight();
        			right++;
        			StepForward();
        			
        		}
        		
        		else if(right>left){
        			RotateLeft();
        			left++;
        			StepForward();
        			
        			
        		}
        		
        		
        		else{
        			Random rand = new Random();
        			int ch = rand.Next(5);
        			
        				if (ch > 2 ){
        				RotateLeft();
        				left++;
        				StepForward();
        			}
        			else{
        				RotateRight();
        			right++;
        			StepForward();
        			}
        			
        		
        		}
        		
        		
        	}
        	
        	
        }
        
        
        static void LookAround()
        {
Console.WriteLine("Rozglądam się dookoła");
Console.WriteLine("---------------------");
agentTomek.Look();
agentTomek.RotateRight();
agentTomek.Look();
agentTomek.RotateRight();
agentTomek.Look();
agentTomek.RotateRight();
agentTomek.Look();
agentTomek.RotateRight();
Console.WriteLine("---------------------");
        }

        // ładujemy się
        private static void Recharge()
        {
            int added = agentTomek.Recharge();
            energy += added;
            Console.WriteLine("Otrzymano " + added + " energii");
        }

        //wysyłamy komunikat
        private static void Speak()
        {
            string input = Console.ReadLine();
         if (energy < cennikSwiata.speakCost)
                Console.WriteLine("Mowienie nie powiodlo sie - brak energii");
            else
                Console.WriteLine(input);
             energy -= cennikSwiata.speakCost;
        }

        //obracamy się w lewo
        private static void RotateLeft() //Switch w RotateLeft (tak samo w RotateRight) obslguje aktualizacje kierunku po kazdym obrocie
        { //jest to pozniej wykorzystywane przy obliczaniu aktualnych wsp. po kazdym ruchu w StepForward
        
if (!agentTomek.RotateLeft())
                Console.WriteLine("Obrot nie powiodl sie - brak energii");
                
            else
                energy -= cennikSwiata.rotateCost;
            
           
        	
/*
        	if (energy < cennikSwiata.rotateCost)
                Console.WriteLine("Obrot nie powiodl sie - brak energii");
            else
            {
                energy -= cennikSwiata.rotateCost;
                
             if (kierunek=="N")
             kierunek="W";

             else if(kierunek=="W")
             kierunek="S";

             else if(kierunek=="S")
             kierunek="E";

             else if(kierunek=="E")
             kierunek="N";
             
            }
            */
        }

        //obracamy się w prawo
        private static void RotateRight()
        {
        	
        	
        	
        	 if (!agentTomek.RotateRight())
                Console.WriteLine("Obrot nie powiodl sie - brak energii");
            else
                energy -= cennikSwiata.rotateCost; /*
        	
            if (energy < cennikSwiata.rotateCost)
                Console.WriteLine("Obrot nie powiodl sie - brak energii");

            else
            {
                energy -= cennikSwiata.rotateCost;

             if (kierunek=="N")
                kierunek="E";

             else if(kierunek=="E")
                kierunek="S";

             else if(kierunek=="S")
                kierunek="W";

             else if(kierunek=="W")
                kierunek="N";
                
            }
            */
        }

        //idziemy do przodu
        private static void StepForward() //teraz teoretycznie (nie sprawdzalem w praktyce) StepForward jest juz w tanie aktualizowac
        { //energie oraz polozenie agenta po kazdym wykonanym ruchu.
        	if (!agentTomek.StepForward()){
        		OminPrzeszkode();
        			Console.WriteLine("Omijam");
        	}
                
            if (energy >= cennikSwiata.moveCost)
                energy -= cennikSwiata.moveCost;
        	
        	
            int y = LiczPoleDocelY(ObecnePolozenieY);
         int x = LiczPoleDocelX(ObecnePolozenieX);
            int[,] poleDocelowe = new int [x,y];
            int[,] poleAgenta = new int [ObecnePolozenieX,ObecnePolozenieY];

   /*      if (energy < cennikSwiata.moveCost * (1 + (poleDocelowe.height - poleAgenta.height)/100))
                Console.WriteLine("Za malo energii Wujku");

         else
         {
         energy -= cennikSwiata.moveCost * (1 + (poleDocelowe.height - poleAgenta.height)/100);

         switch(kierunek)
             {
             case "N":
             ObecnePolozenieY += 1;
             break;
             case "S":
             ObecnePolozenieY -= 1;
             break;
             case "W":
             ObecnePolozenieX -= 1;
             break;
             case "E":
             ObecnePolozenieX += 1;
             break;
             }
         }
         */
        Recharge();
        Recharge();
        }

        private static int LiczPoleDocelY(int Y)
        {
            switch(kierunek)
            {
             case "N":
            // poleDoceloweY=Y + 1;
             //poleDoceloweX=ObecnePolozenieX;
             break;
             case "S":
           //  poleDoceloweY=Y - 1;
             //poleDoceloweX=ObecnePolozenieX;
             break;
             /*case "W":
poleDoceloweX=ObecnePolozenieX - 1;
poleDoceloweY=ObecnePolozenieY;
break;
case "E":
poleDoceloweX=ObecnePolozenieX + 1;
poleDoceloweY=ObecnePolozenieY;
break;
*/
            }
            return 0;
        }

        private static int LiczPoleDocelX(int X)
        {
            switch(kierunek)
            {
             /*case "N":
poleDoceloweY=ObecnePolozenieY + 1;
//poleDoceloweX=ObecnePolozenieX;
break;
case "S":
poleDoceloweY=ObecnePolozenieY - 1;
//poleDoceloweX=ObecnePolozenieX;
break;*/
                case "W":
              //  poleDoceloweX = X - 1;
                //poleDoceloweY=ObecnePolozenieY;
                break;
                case "E":
                  //  poleDoceloweX = X + 1;
            //poleDoceloweY=ObecnePolozenieY;
               break;
                    }
                    return 0;
                }

        private static void Look()
                {
                    OrientedField[] pola = agentTomek.Look(); //dostajemy listę pól które widzi nasz agent

                    //wyświetlamy informacje o wszystkich widzianych polach
                    foreach (OrientedField pole in pola)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("POLE " + LiczPoleDocelX(ObecnePolozenieX) + "," + LiczPoleDocelY(ObecnePolozenieY)); //pole.x pole.y
                        Console.WriteLine("Wysokosc: " + pole.height); // tutaj zdaje sie moze tak zostac, nomenklatura nie ma znaczenia
                        if (pole.energy != 0) // dla danego pola wysokosc jest bezwzgledna
                            Console.WriteLine("Energia: " + pole.energy); // ---||---
                        if (pole.obstacle)
                            Console.WriteLine("Przeszkoda");
                        if (pole.agent != null)
                            Console.WriteLine("Agent" + pole.agent.agentname + " i jest obrocony na " + pole.agent.direction.ToString());
                        Console.WriteLine("-----------------------------");
                    }
                }
        
        

        

    }
}

