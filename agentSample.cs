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
        static string kierunek="N"; // jako domyslna wart. przyjmuje N, nie jest wazne jaki kierunek jest to faktycznie
         // bo wykorzystujemy to tylko do wlasnych potrzeb.
         // pozniej przyjmuje W, N, E, S - potrzebne od zapamietywania kierunku -> modyfikacji polozenia
        static string gimie = "";
        static int aktualne_x=0;
        static int aktualne_y=0;
		
		
       	
       	
        // Nasza metoda nasłuchująca
        static void Listen(String krzyczacyAgent, String komunikat) {
            Console.WriteLine(krzyczacyAgent + " krzyczy " + komunikat);
            getOutput(komunikat);
              agentTomek.Speak(Console.ReadLine(), 1);
            //ConReadline();
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
                
                 gimie = imie;

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
                    pamiec.tabela[100,100].set_odwiedzone(true); //odwiedzanie pola startowego
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
                    case ConsoleKey.G: ConReadline();
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
        
        
        
        //Czyta linie w poszukiwaniu krzyczy i zwraca komunikat
        static void ConReadline()
        {
        	string str = Console.ReadLine();
        	
        	if (!str.StartsWith(gimie))
        	{
        		
        	int first = str.IndexOf("krzyczy");
        	int last = str.Length;
            string str2 = str.Substring(first+7, last);
        	getOutput(str2);
        	    	
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
            if (!agentTomek.Speak(Console.ReadLine(), 1))
                Console.WriteLine("Mowienie nie powiodlo sie - brak energii");
            else
                energy -= cennikSwiata.speakCost;
        }

        //obracamy się w lewo
        private static void RotateLeft() //Switch w RotateLeft (tak samo w RotateRight) obslguje aktualizacje kierunku po kazdym obrocie
        { 
        
if (!agentTomek.RotateLeft())
                Console.WriteLine("Obrot nie powiodl sie - brak energii");
                
else{
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
           
        }
        
        
       // pamiec.tabela[100+PoleR().wspolrzedna_x,100].set_wysokosc
     
       private static Wspolrzedne PoleR() //zwraca pole po prawej stronie agenta
        {
        	if (kierunek=="N"){
        		Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x+1;
        		wsp.wspolrzedna_y=aktualne_y;
        	 	return wsp;
        	}
                

       	else if(kierunek=="E"){
       		    Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x;
        		wsp.wspolrzedna_y=aktualne_y-1;
        	 	return wsp;
       	}
                

       	else if(kierunek=="S"){
       		    Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x-1;
        		wsp.wspolrzedna_y=aktualne_y;
        	 	return wsp;
       	}
                

       	else if(kierunek=="W"){
       			Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x;
        		wsp.wspolrzedna_y=aktualne_y+1;
        	 	return wsp;
       	}
       	
       			Wspolrzedne wsp1;
       			wsp1.wspolrzedna_x=aktualne_x;
        		wsp1.wspolrzedna_y=aktualne_y;
                return wsp1;
        }
        
        private static void PoleL() //zwraca pole po lewej stronie agenta
        {
        	if (kierunek=="N"){
        		Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x-1;
        		wsp.wspolrzedna_y=aktualne_y;
        	 	return wsp;
        	}
                

       	else if(kierunek=="E"){
       		    Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x;
        		wsp.wspolrzedna_y=aktualne_y+1;
        	 	return wsp;
       	}
                

       	else if(kierunek=="S"){
       		    Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x+1;
        		wsp.wspolrzedna_y=aktualne_y;
        	 	return wsp;
       	}
                

       	else if(kierunek=="W"){
       			Wspolrzedne wsp;
        		wsp.wspolrzedna_x=aktualne_x;
        		wsp.wspolrzedna_y=aktualne_y-1;
        	 	return wsp;
       	}
       	
       			Wspolrzedne wsp1;
       			wsp1.wspolrzedna_x=aktualne_x;
        		wsp1.wspolrzedna_y=aktualne_y;
                return wsp1;
        }
        

        //obracamy się w prawo
        private static void RotateRight()
        {
        	       	        	
        	 if (!agentTomek.RotateRight())
                Console.WriteLine("Obrot nie powiodl sie - brak energii");
        	 else{
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
            
        }

        //idziemy do przodu
        private static void StepForward() 
        { 
        	if (!agentTomek.StepForward()){
        		OminPrzeszkode();
        			Console.WriteLine("Omijam");
        	}
                
            if (energy >= cennikSwiata.moveCost)
                energy -= cennikSwiata.moveCost;
        	System.Threading.Thread.Sleep(1000);
        	Look();
        	Recharge();
        	
        	if(kierunek=="N")
        		aktualne_y++;
        	else if(kierunek=="S")
        		aktualne_y--;
        	else if(kierunek=="W")
        		aktualne_x--;
        	else if(kierunek=="E")
        		aktualne_x++;
        	
        	
			pamiec.tabela[100+aktualne_x,100+aktualne_y].set_odwiedzone(true);
        		
        
        
        }

        
        static Mapa pamiec = new CsClient.Mapa();
        

        private static void Look() {
	            OrientedField[] pola = agentTomek.Look();
	            foreach (OrientedField pole in pola)
	            {
	                Console.WriteLine("-----------------------------");
	                Console.WriteLine("POLE " + pole.x + "," + pole.y);
	                Console.WriteLine("Wysokosc: " + pole.height);
	                pamiec.tabela[100+pole.x,100+pole.y].set_wysokosc(pole.height);
					pamiec.tabela[100+pole.x,100+pole.y].set_poznane(true);	                
	              
	              if (pole.energy != 0){
	                    Console.WriteLine("Energia: " + pole.energy);
	                    pamiec.tabela[100+pole.x,100+pole.y].set_niepewne_zrodlo(true);
	               		//Console.WriteLine(pamiec.tabela[100+pole.x,100+pole.y].get_niepewne_zrodlo());
	              }
	              if (pole.obstacle){
	                    Console.WriteLine("Przeszkoda");
	                    pamiec.tabela[100+pole.x,100+pole.y].set_przeszkoda(true);     
	              }
	                if (pole.agent != null)
	                    Console.WriteLine("Agent " + pole.agent.fullName + " i jest obrocony na " + pole.agent.direction.ToString());
	                Console.WriteLine("-----------------------------");
         	   }
                 }
         
        

        

    }
}

