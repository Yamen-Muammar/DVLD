using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD__Core.Models;
using DVLD__Data_Tier.Repositories;

namespace DVLD__Business_Tier.Services
{
    public class TestTypeService
    {
        private TestTypesRepository _repository;

        public TestTypeService()
        {
            _repository = new TestTypesRepository();
        }
        public  async Task<TestType> FindAsync(int id)
        {
            TestType testType = await _repository.GetTestTypeByID(id);
            
            return testType;
        }
        public  async Task<bool> UpdateTestTypeAsync(TestType testType)
        {
            if (!_validateTestType(testType))
            {
                return false;
            }
            
            if (!await _repository.UpdateTestType(testType))
            {
                throw new Exception("Can not update the test type. Please try again later.");
            }
            return true;
        }
        public  async Task<List<TestType>> GetAllTestTypesAsync()
        {
            List<TestType> testTypes; 

            testTypes = await _repository.GetAllTestTypes();
            
            return testTypes;
        }
        private bool _validateTestType(TestType testType)
        {
            if (testType == null)
            {
                throw new ArgumentNullException(nameof(testType), "TEST type cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(testType.TestTypeTitle))
            {
                throw new ArgumentException("TEST type title cannot be empty.", nameof(testType));
            }
            if (testType.TestTypeFees < 0)
            {
                throw new ArgumentException("TEST type fees cannot be negative.", nameof(testType));
            }
            return true;
        }


    }
}
