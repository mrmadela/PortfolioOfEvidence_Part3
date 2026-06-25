using System.Collections;

namespace PortfolioOfEvidence_Part3
{// start of namespace
    public class respond
    {// start of a response class

        public respond(ArrayList reply, ArrayList ignore)
        {//start of constructor

            answers(reply);
            words(ignore);


        }//end of constructor

        //method to store the list of words
        private void words(ArrayList ignoring)
        {// start of words method

            //ignoring questions
            ignoring.Add("a");
            ignoring.Add("about");
            ignoring.Add("above");
            ignoring.Add("across");
            ignoring.Add("after");
            ignoring.Add("afterwards");
            ignoring.Add("again");
            ignoring.Add("against");
            ignoring.Add("all");
            ignoring.Add("almost");
            ignoring.Add("alone");
            ignoring.Add("along");
            ignoring.Add("already");
            ignoring.Add("also");
            ignoring.Add("although");
            ignoring.Add("always");
            ignoring.Add("am");
            ignoring.Add("among");
            ignoring.Add("amongst");
            ignoring.Add("amount");
            ignoring.Add("an");
            ignoring.Add("and");
            ignoring.Add("another");
            ignoring.Add("any");
            ignoring.Add("anyhow");
            ignoring.Add("anyone");
            ignoring.Add("anything");
            ignoring.Add("anyway");
            ignoring.Add("anywhere");
            ignoring.Add("are");
            ignoring.Add("around");
            ignoring.Add("as");
            ignoring.Add("at");
            ignoring.Add("back");
            ignoring.Add("be");
            ignoring.Add("became");
            ignoring.Add("because");
            ignoring.Add("become");
            ignoring.Add("becomes");
            ignoring.Add("becoming");
            ignoring.Add("been");
            ignoring.Add("before");
            ignoring.Add("beforehand");
            ignoring.Add("behind");
            ignoring.Add("being");
            ignoring.Add("below");
            ignoring.Add("beside");
            ignoring.Add("besides");
            ignoring.Add("between");
            ignoring.Add("beyond");
            ignoring.Add("both");
            ignoring.Add("but");
            ignoring.Add("by");
            ignoring.Add("can");
            ignoring.Add("cannot");
            ignoring.Add("could");
            ignoring.Add("did");
            ignoring.Add("do");
            ignoring.Add("does");
            ignoring.Add("doing");
            ignoring.Add("done");
            ignoring.Add("down");
            ignoring.Add("during");
            ignoring.Add("each");
            ignoring.Add("either");
            ignoring.Add("else");
            ignoring.Add("elsewhere");
            ignoring.Add("enough");
            ignoring.Add("etc");
            ignoring.Add("even");
            ignoring.Add("ever");
            ignoring.Add("every");
            ignoring.Add("everyone");
            ignoring.Add("everything");
            ignoring.Add("everywhere");
            ignoring.Add("except");
            ignoring.Add("few");
            ignoring.Add("first");
            ignoring.Add("for");
            ignoring.Add("former");
            ignoring.Add("formerly");
            ignoring.Add("from");
            ignoring.Add("further");
            ignoring.Add("had");
            ignoring.Add("has");
            ignoring.Add("have");
            ignoring.Add("having");
            ignoring.Add("he");
            ignoring.Add("hence");
            ignoring.Add("her");
            ignoring.Add("here");
            ignoring.Add("hereafter");
            ignoring.Add("hereby");
            ignoring.Add("herein");
            ignoring.Add("hereupon");
            ignoring.Add("hers");
            ignoring.Add("herself");
            ignoring.Add("him");
            ignoring.Add("himself");
            ignoring.Add("his");
            ignoring.Add("how");
            ignoring.Add("however");
            ignoring.Add("i");
            ignoring.Add("if");
            ignoring.Add("in");
            ignoring.Add("indeed");
            ignoring.Add("inside");
            ignoring.Add("instead");
            ignoring.Add("into");
            ignoring.Add("is");
            ignoring.Add("it");
            ignoring.Add("its");
            ignoring.Add("itself");
            ignoring.Add("last");
            ignoring.Add("later");
            ignoring.Add("latter");
            ignoring.Add("latterly");
            ignoring.Add("least");
            ignoring.Add("less");
            ignoring.Add("lot");
            ignoring.Add("many");
            ignoring.Add("may");
            ignoring.Add("me");
            ignoring.Add("meanwhile");
            ignoring.Add("might");
            ignoring.Add("more");
            ignoring.Add("moreover");
            ignoring.Add("most");
            ignoring.Add("mostly");
            ignoring.Add("much");
            ignoring.Add("must");
            ignoring.Add("my");
            ignoring.Add("myself");
            ignoring.Add("name");
            ignoring.Add("namely");
            ignoring.Add("neither");
            ignoring.Add("never");
            ignoring.Add("nevertheless");
            ignoring.Add("next");
            ignoring.Add("no");
            ignoring.Add("nobody");
            ignoring.Add("none");
            ignoring.Add("noone");
            ignoring.Add("nor");
            ignoring.Add("not");
            ignoring.Add("nothing");
            ignoring.Add("now");
            ignoring.Add("nowhere");
            ignoring.Add("of");
            ignoring.Add("off");
            ignoring.Add("often");
            ignoring.Add("on");
            ignoring.Add("once");
            ignoring.Add("one");
            ignoring.Add("only");
            ignoring.Add("or");
            ignoring.Add("other");
            ignoring.Add("others");
            ignoring.Add("otherwise");
            ignoring.Add("ought");
            ignoring.Add("our");
            ignoring.Add("ours");
            ignoring.Add("ourselves");
            ignoring.Add("out");
            ignoring.Add("outside");
            ignoring.Add("over");
            ignoring.Add("own");
            ignoring.Add("part");
            ignoring.Add("per");
            ignoring.Add("perhaps");
            ignoring.Add("please");
            ignoring.Add("put");
            ignoring.Add("rather");
            ignoring.Add("re");
            ignoring.Add("same");
            ignoring.Add("see");
            ignoring.Add("seem");
            ignoring.Add("seemed");
            ignoring.Add("seeming");
            ignoring.Add("seems");
            ignoring.Add("several");
            ignoring.Add("she");
            ignoring.Add("should");
            ignoring.Add("show");
            ignoring.Add("side");
            ignoring.Add("since");
            ignoring.Add("so");
            ignoring.Add("some");
            ignoring.Add("somehow");
            ignoring.Add("someone");
            ignoring.Add("something");
            ignoring.Add("sometime");
            ignoring.Add("sometimes");
            ignoring.Add("somewhere");
            ignoring.Add("still");
            ignoring.Add("such");
            ignoring.Add("take");
            ignoring.Add("than");
            ignoring.Add("that");
            ignoring.Add("the");
            ignoring.Add("their");
            ignoring.Add("theirs");
            ignoring.Add("them");
            ignoring.Add("themselves");
            ignoring.Add("then");
            ignoring.Add("thence");
            ignoring.Add("there");
            ignoring.Add("thereafter");
            ignoring.Add("thereby");
            ignoring.Add("therefore");
            ignoring.Add("therein");
            ignoring.Add("thereupon");
            ignoring.Add("these");
            ignoring.Add("they");
            ignoring.Add("this");
            ignoring.Add("those");
            ignoring.Add("though");
            ignoring.Add("through");
            ignoring.Add("throughout");
            ignoring.Add("thru");
            ignoring.Add("thus");
            ignoring.Add("to");
            ignoring.Add("together");
            ignoring.Add("too");
            ignoring.Add("toward");
            ignoring.Add("towards");
            ignoring.Add("under");
            ignoring.Add("unless");
            ignoring.Add("until");
            ignoring.Add("up");
            ignoring.Add("upon");
            ignoring.Add("us");
            ignoring.Add("used");
            ignoring.Add("very");
            ignoring.Add("via");
            ignoring.Add("was");
            ignoring.Add("we");
            ignoring.Add("well");
            ignoring.Add("were");
            ignoring.Add("what");
            ignoring.Add("whatever");
            ignoring.Add("when");
            ignoring.Add("whence");
            ignoring.Add("whenever");
            ignoring.Add("where");
            ignoring.Add("whereafter");
            ignoring.Add("whereas");
            ignoring.Add("whereby");
            ignoring.Add("wherein");
            ignoring.Add("whereupon");
            ignoring.Add("wherever");
            ignoring.Add("whether");
            ignoring.Add("which");
            ignoring.Add("while");
            ignoring.Add("whither");
            ignoring.Add("who");
            ignoring.Add("whoever");
            ignoring.Add("whole");
            ignoring.Add("whom");
            ignoring.Add("whose");
            ignoring.Add("why");
            ignoring.Add("will");
            ignoring.Add("with");
            ignoring.Add("within");
            ignoring.Add("without");
            ignoring.Add("would");
            ignoring.Add("yes");
            ignoring.Add("yet");
            ignoring.Add("hey");
            ignoring.Add("you");
            ignoring.Add("your");
            ignoring.Add("yours");
            ignoring.Add("yourself");
            ignoring.Add("yourselves");

        }// end of word method


        // method to store the list of answers
        public void answers(ArrayList add_answers)
        {//start of method


            // answers about to greet the user
            add_answers.Add("greeting: i'm doing well, thank you for asking. how can i help you today?");
            add_answers.Add("greeting: hello! i'm here and ready to help you with cybersecurity questions.");
            add_answers.Add("greeting: hi there! i'm doing great today. what would you like to know?");
            add_answers.Add("greeting: welcome! i hope you're having a good day. how may i assist you?");
            add_answers.Add("greeting: hello! feel free to ask me anything about online safety and security.");

            // answers about the purpose of the chatbot
            add_answers.Add("purpose: my purpose is to help users understand cybersecurity and stay safe online.");
            add_answers.Add("purpose: i provide guidance about online safety, scams, and digital protection.");
            add_answers.Add("purpose: i educate users about cybersecurity threats and safe internet practices.");
            add_answers.Add("purpose: i help answer cybersecurity questions and provide safety tips.");
            add_answers.Add("purpose: my goal is to improve awareness about online security and privacy.");

            // answers about the cyber security topic
            add_answers.Add("cybersecurity: cybersecurity is the practice of protecting systems, networks, and data from cyber threats.");
            add_answers.Add("cybersecurity: it involves securing devices, accounts, and information from unauthorized access.");
            add_answers.Add("cybersecurity: cybersecurity helps protect people and organizations from online attacks.");
            add_answers.Add("cybersecurity: it focuses on preventing hacking, malware, phishing, and data breaches.");
            add_answers.Add("cybersecurity: good cybersecurity practices help keep your personal information safe online.");

            // answers about the phishing topic
            add_answers.Add("phishing: phishing is a cyberattack where scammers pretend to be trusted sources to steal sensitive information.");
            add_answers.Add("phishing: attackers often use fake emails, messages, or websites to trick users.");
            add_answers.Add("phishing: phishing scams may ask for passwords, banking details, or personal information.");
            add_answers.Add("phishing: always check links and sender addresses carefully before sharing information.");
            add_answers.Add("phishing: avoid clicking suspicious links or downloading unknown attachments.");

            // answers about the firewall topic
            add_answers.Add("firewall: a firewall monitors and controls incoming and outgoing network traffic.");
            add_answers.Add("firewall: it acts as a security barrier between your device and potential online threats.");
            add_answers.Add("firewall: firewalls help block unauthorized access to your computer or network.");
            add_answers.Add("firewall: using a firewall improves protection against hackers and malware.");
            add_answers.Add("firewall: both hardware and software firewalls can help secure your system.");

            // answers about the password topic
            add_answers.Add("password: a strong password helps protect your accounts and personal information.");
            add_answers.Add("password: use a combination of letters, numbers, and symbols when creating passwords.");
            add_answers.Add("password: avoid using personal details such as birthdays or names in your password.");
            add_answers.Add("password: using unique passwords for different accounts improves your security.");
            add_answers.Add("password: changing passwords regularly can help reduce security risks.");


            // answers about the hacking account topic
            add_answers.Add("hacked account: change your password immediately if you believe your account was compromised.");
            add_answers.Add("hacked account: enable two-factor authentication to improve account security.");
            add_answers.Add("hacked account: review recent account activity for suspicious actions or logins.");
            add_answers.Add("hacked account: contact the platform support team if you cannot access your account.");
            add_answers.Add("hacked account: log out of all devices after securing your account.");

            // answers about fraud
            add_answers.Add("fraud: contact your bank immediately if you notice suspicious financial activity.");
            add_answers.Add("fraud: monitor your accounts regularly for unauthorized transactions.");
            add_answers.Add("fraud: report fraud to the proper authorities or financial institutions.");
            add_answers.Add("fraud: never share sensitive banking information with unknown individuals.");
            add_answers.Add("fraud: using secure websites and trusted payment methods can reduce fraud risks.");

            // answers about maliware topic
            add_answers.Add("malicious chatbot: fake chatbots may try to steal personal or financial information.");
            add_answers.Add("malicious chatbot: be cautious if a chatbot pressures you to act quickly.");
            add_answers.Add("malicious chatbot: avoid sharing passwords or banking details with unknown bots.");
            add_answers.Add("malicious chatbot: trusted services usually do not ask for sensitive information through chat.");
            add_answers.Add("malicious chatbot: always verify that the chatbot belongs to an official company or website.");

            // answers about vpn
            add_answers.Add("vpn: a vpn encrypts your internet connection to improve online privacy.");
            add_answers.Add("vpn: using a vpn can help protect your data on public wi-fi networks.");
            add_answers.Add("vpn: a vpn hides your ip address and makes online activity more secure.");
            add_answers.Add("vpn: vpn services can help reduce the risk of data interception.");
            add_answers.Add("vpn: choose a trusted vpn provider for better privacy and security.");


            //sentiment detection

            // answers when the user is feeling frustrated
            add_answers.Add("frustrated: i understand this situation can be frustrating. i'll help you step by step.");
            add_answers.Add("frustrated: don't worry, we'll work through the issue together.");
            add_answers.Add("frustrated: technical problems can be stressful, but i'll do my best to assist you.");
            add_answers.Add("frustrated: i understand your frustration. let's try to solve the problem calmly.");
            add_answers.Add("frustrated: i'm here to help you find a solution as quickly as possible.");

            // answers when the user is confused
            add_answers.Add("confused: that's okay. i'll explain it in a simpler way.");
            add_answers.Add("confused: let me break the information down step by step for you.");
            add_answers.Add("confused: cybersecurity topics can be confusing at first, but i'll help you understand.");
            add_answers.Add("confused: no problem, feel free to ask questions if something is unclear.");
            add_answers.Add("confused: i'll try to explain the topic more clearly and simply.");

            // ansers when the user is worried
            add_answers.Add("worried: i understand your concern, and i'll help you stay safe online.");
            add_answers.Add("worried: try not to panic. many cybersecurity issues can be resolved.");
            add_answers.Add("worried: taking quick action can help protect your information and accounts.");
            add_answers.Add("worried: i'm here to guide you through the situation step by step.");
            add_answers.Add("worried: securing your accounts and passwords is a good first step.");

            // ansers when the user is happy
            add_answers.Add("happy: that's wonderful to hear! i'm glad things are going well.");
            add_answers.Add("happy: great! i'm happy that everything is working out for you.");
            add_answers.Add("happy: awesome! let me know if you need any more help.");
            add_answers.Add("happy: it's nice to hear positive news. keep staying safe online.");
            add_answers.Add("happy: i'm glad you're feeling good today!");

            //answers when the user is sad
            add_answers.Add("sad: i'm sorry you're feeling this way. i'm here to support you.");
            add_answers.Add("sad: take things one step at a time. things can improve.");
            add_answers.Add("sad: difficult situations can happen, but you're not alone.");
            add_answers.Add("sad: i hope your day gets better soon. feel free to talk anytime.");
            add_answers.Add("sad: i'm here to help if you need guidance or support.");

            // answers when the user is angry
            add_answers.Add("angry: i understand you're upset. let's work together to solve the issue.");
            add_answers.Add("angry: i'll do my best to help you fix the problem quickly.");
            add_answers.Add("angry: it's understandable to feel angry when something goes wrong.");
            add_answers.Add("angry: let's focus on finding a solution step by step.");
            add_answers.Add("angry: i'm here to assist you and help resolve the situation.");

        }//end of method



    }// end of response class
}// end of namespace