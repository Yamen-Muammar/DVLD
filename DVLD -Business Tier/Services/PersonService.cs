using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class PersonService
    {
        
        public static int AddPerson(Person person)
        {
            int NewPersonID = -1;

            if (PersonRepository.IsPersonExist(person.NationalNO))
            {
                return NewPersonID;
            }

            if (!IsPersonInfoValid(person))
            {
                return NewPersonID;
            }

            // Image Handling
            person.ImageName = SetImageProcess(person);
            if (string.IsNullOrEmpty(person.ImageName))
            {
                return NewPersonID;
            }

            // # Save the person to the database here and return true if successful
            
            NewPersonID = PersonRepository.AddNewPerson(person);
            return NewPersonID;
        }

        public static Person Find(int id)
        {
            return PersonRepository.GetPersonByID(id);
        }

        public static List<Person> GetAll()
        {
            return PersonRepository.GetAllPeople();
        }

        public static bool Delete(int id)
        {
            if (DeleteImage(id))
            {
                return PersonRepository.DeletePerson(id);
            }
            return false;
        }

        public static bool Update(Person person)
        {
            if (!IsPersonInfoValid(person))
            {
                return false;
            }
            // Image Handling
            
            bool isPersonNOTUpdateImage = person.ImageName.Length == 40;
            if (!isPersonNOTUpdateImage)
            {
                person.ImageName = SetImageProcess(person);
                if (string.IsNullOrEmpty(person.ImageName))
                {
                    return false;
                }
            }
            return PersonRepository.UpdatePerson(person);
        }

        //Handeling Image Saving and Deletion
        private static string SetImageProcess(Person person)
        {
            
            if (string.IsNullOrEmpty(person.ImageName))
            {
                return string.Empty;
            }

            string fileExtension = Path.GetExtension(person.ImageName);
            string NewImageName = Guid.NewGuid().ToString() + fileExtension;

            string NewImageDestinationPath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", NewImageName);
            try
            {
                if (DeleteImage(person.PersonID))
                {
                    File.Copy(person.ImageName, NewImageDestinationPath, true);
                    return NewImageName;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*** Error copying image: " + ex.Message + "***");                
            }
            return string.Empty;
        }
        private static bool DeleteImage(int PersonId)
        {
            if (PersonId == -1 || PersonId == 0)
            {
                return true;
            }            

            Person person = Find(PersonId);
            if (person == null)
            {
                return false;
            }

            string oldImageName = person.ImageName;
            //TODO:We have a problem with access the file.(we can update the photo for Update Users.)
            string DeleteDestinationPath = Path.Combine(@"F:\yamen - 2024\C#\Course\projects\PersonPic", oldImageName);
            try
            {
                File.Delete($@"{DeleteDestinationPath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*** Error deleting image: " + ex.Message + "***");
                return false;
            }
        }

        private static bool IsPersonInfoValid(Person person)
        {
         
            if (string.IsNullOrEmpty(person.FirstName) || string.IsNullOrEmpty(person.MiddelName)
                || string.IsNullOrEmpty(person.ThirdName)|| string.IsNullOrEmpty(person.LastName)
                ||string.IsNullOrEmpty(person.NationalNO)||string.IsNullOrEmpty(person.Email)||
                string.IsNullOrEmpty(person.Address) || string.IsNullOrEmpty(person.ImageName))
            {
                return false;
            }

            bool isAgeValid = DateTime.Now.Year - person.DateOfBirth.Year >= 18;
            if (!isAgeValid)
            {
                return false;
            }


            return true;
        }
    }
}
