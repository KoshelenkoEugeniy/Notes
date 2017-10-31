using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ViewDescription;
using ModelDescription;
using RepositoryDescription;

namespace Notes
{
    class Program
    {

       static void Main(string[] args)
        {
            View blView = new View();

            Model blModel = new Model();

            string userAnswer = "";
            string noteText = "";
            string modelAnswer = "";
            
            while (userAnswer != "E" && userAnswer != "e")
            {
                
                userAnswer = WriteInfo_ReadAnswer(Menu(0));

                switch (userAnswer)
                {
                    case "C":
                    case "c":

                        noteText = WriteInfo_ReadAnswer(Menu(4));

                        modelAnswer = blModel.ToDo("Create", noteText);

                        CheckModelAnswer(modelAnswer);

                        break;

                    case "D":
                    case "d":
                        
                        noteText = WriteInfo_ReadAnswer(Menu(1)); ;

                        try
                        {
                            int.Parse(noteText);
                        }
                        catch
                        {
                            blView.ShowInfo(Menu(-1));
                            break;
                        }

                        modelAnswer = blModel.ToDo("Delete", noteText);

                        CheckModelAnswer(modelAnswer);

                        break;


                    case "S":
                    case "s":

                        noteText = WriteInfo_ReadAnswer(Menu(2));

                        modelAnswer = blModel.ToDo("Search", noteText);

                        blView.ShowInfo(modelAnswer);

                        break;


                    case "U":
                    case "u":

                        noteText = WriteInfo_ReadAnswer(Menu(1));

                        string newStatus = "";

                        string newText = "";

                        string noteNumber = "";

                        bool shouldChange = false;

                        try
                        {
                            int.Parse(noteText);
                        }
                        catch
                        {
                            blView.ShowInfo(Menu(-1));
                            break;
                        }

                        noteNumber = noteText;

                        modelAnswer = blModel.ToDo("GetStatus", noteNumber);

                        if (modelAnswer == "Current" || modelAnswer == "Finished")
                        {
                            noteText = WriteInfo_ReadAnswer(Menu(5, modelAnswer));

                            if (noteText == "Y" || noteText == "y")
                            {
                                if (modelAnswer == "Current")
                                {
                                    newStatus = "Finished";
                                }
                                else
                                {
                                    newStatus = "Current";
                                }

                                shouldChange = true;
                            }
                            else
                            {
                                newStatus = modelAnswer;
                            }
                        }
                        else
                        {
                            blView.ShowInfo("Wrong status!");
                            break;
                        }

                        modelAnswer = blModel.ToDo("GetNote", noteNumber);

                        noteText = WriteInfo_ReadAnswer(Menu(6, modelAnswer));

                        if (noteText == "Y" || noteText == "y")
                        {
                            newText = WriteInfo_ReadAnswer(Menu(4));

                            shouldChange = true;
                        }
                        else
                        {
                            if (shouldChange == true) newText = modelAnswer;
                        }

                        if (shouldChange == true)
                        {
                            modelAnswer = blModel.ToDo("Change", noteNumber, newStatus, newText);

                            CheckModelAnswer(modelAnswer);

                        }

                        break;

                    default:
                        blView.ShowInfo(Menu(-1));
                        break;
                }
            }


            void CheckModelAnswer(string answer)
            {
                if (answer == "ok")
                {
                    blView.ShowInfo(Menu(3));
                }
                else
                {
                    blView.ShowInfo(modelAnswer);
                }
            }


            string WriteInfo_ReadAnswer(string text)
            {
                blView.ShowInfo(text);
                return blView.GetInfo();
            }

            
            string Menu(int number, string additionalInfo = "")
            {
                switch (number)
                {
                    case 0:
                        return " Please choose necessary command to be applied to notes: \r \n C: create   D: delete    S: search     U: update     E: exit \r \n";
                    case 1:
                        return " Please type a note's number: \r \n";
                    case 2:
                        return " Please type a key word, that should be found: \r \n";
                    case 3:
                        return " Operation successfully done. \r \n";
                    case 4:
                        return " Please type a note: \r \n";
                    case 5:
                        return $" Current status is: {additionalInfo}. Should the status be changed? Y/N: \r \n";
                    case 6:
                        return $" Current note is: {additionalInfo}. Should the note be changed? Y/N: \r \n";
                    default:
                        return " Wrong command. Please retype... \r \n";
                }
            }
            
        }
    }
}
