using AutoMapper;
using SMovie.Application.Extension;
using SMovie.Application.Helper;
using SMovie.Application.IService;
using SMovie.Application.MessageService;
using SMovie.Domain.Constants;
using SMovie.Domain.Entity;
using SMovie.Domain.Enum;
using SMovie.Domain.Models;
using SMovie.Domain.UnitOfWork;
using SMovie.Infrastructure.UnitOfWork;
using System.Net;

namespace SMovie.Application.Service
{
    public class PersonService : IPersonService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public PersonService(IMapper mapper)
		{
			_unitOfWork = new UnitOfWork();
			_mapper = mapper;
		}

		public async Task<ResponseDTO> CreatePerson(NewPerson newPerson)
		{
			// Create new id for person
			newPerson.PersonId = Guid.NewGuid();

			// Validate data
			ResponseDTO responseDTO = await ValidateData(newPerson);

			// If validate fail
			if (responseDTO.Status != HttpStatusCode.Continue)
			{
				return responseDTO;
			}

			// Map old person to new person
			Person person = _mapper.Map<Person>(newPerson);
			person.DateCreated = DateTime.Now;

			// Add new person to database
			await _unitOfWork.PersonRepository.Add(person);

			if (await _unitOfWork.SaveChangesAsync())
			{
				return new ResponseDTO(HttpStatusCode.Created, MessageCommon.SavingSuccesfully, newPerson.PersonId);
			}

			return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageSystem.ServerError);
		}

		public async Task<ResponseDTO> UpdatePerson(NewPerson newPerson)
		{
			// Get person by id
			Person? person = await _unitOfWork.PersonRepository.GetById(newPerson.PersonId);

			// If person not found
			if (person == null)
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessagePerson.PersonNotFound);
			}

			// Validate data
			ResponseDTO responseDTO = await ValidateData(newPerson);

			// If validate fail
			if (responseDTO.Status != HttpStatusCode.Continue)
			{
				return responseDTO;
			}

			// Map old person to new person
			person = _mapper.Map<Person>(newPerson);

			// Update person
			_unitOfWork.PersonRepository.Update(person);

			if (await _unitOfWork.SaveChangesAsync())
			{
				return new ResponseDTO(HttpStatusCode.OK, MessageCommon.UpdateSuccesfully);
			}

			return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageSystem.ServerError);
		}

		private async Task<ResponseDTO> ValidateData(NewPerson newPerson)
		{
			// Check nation
			newPerson.NationId = await _unitOfWork.NationRepository.CheckNation(newPerson.NationId);
			if (string.IsNullOrEmpty(newPerson.NationId))
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessageCommon.NationNotFound);
			}

			// Check role
			newPerson.Role = CheckNameConstant.CheckRoleName(newPerson.Role);
			if (string.IsNullOrEmpty(newPerson.Role))
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessagePerson.RoleMustBeValidFormat);
			}

			// Check exist person name
			if (await _unitOfWork.PersonRepository.CheckExistPerson(newPerson.NamePerson, newPerson.PersonId, newPerson.Role))
			{
				return new ResponseDTO(HttpStatusCode.Conflict, MessagePerson.PersonNameExist);
			}

			return new ResponseDTO(HttpStatusCode.Continue, MessageCommon.ValidateSuccessfully, "");
		}

		public async Task<ResponseDTO> DeletePerson(Guid id)
		{
			// Get person by id
			Person? person = await _unitOfWork.PersonRepository.GetById(id);

			// If person not found
			if (person == null)
			{
				return new ResponseDTO(HttpStatusCode.NotFound, MessagePerson.PersonNotFound);
			}

			// Delete person
			await _unitOfWork.PersonRepository.Delete(person);

			if (await _unitOfWork.SaveChangesAsync())
			{
				return new ResponseDTO(HttpStatusCode.OK, MessageCommon.DeleteSuccessfully);
			}

			return new ResponseDTO(HttpStatusCode.ServiceUnavailable, MessageSystem.ServerError);
		}

		public PagedList<Person> GetActors(int page, int eachPage, PersonSortType sortBy)
		{
			if(sortBy == PersonSortType.NamePerson)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.Role!.Equals(Role.RoleActor), page, eachPage, PersonSortBy.NamePerson);
			}
			else if(sortBy == PersonSortType.BirthDate)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.Role!.Equals(Role.RoleActor), page, eachPage, PersonSortBy.BirthDate);
			}
			else if(sortBy == PersonSortType.DateCreated)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.Role!.Equals(Role.RoleActor), page, eachPage, PersonSortBy.DateCreated);
			}

			throw new NotFoundException(MessageException.SortByNotFound);
		}

		public async Task<Person?> GetPerson(Guid id)
		{
			return await _unitOfWork.PersonRepository.GetById(id);
		}

		public PagedList<Person> GetPersons(int page, int eachPage, PersonSortType sortBy)
		{
			if(sortBy == PersonSortType.NamePerson)
			{
				return _unitOfWork.PersonRepository.GetAll(page, eachPage, PersonSortBy.NamePerson);
			}
			else if(sortBy == PersonSortType.BirthDate)
			{
				return _unitOfWork.PersonRepository.GetAll(page, eachPage, PersonSortBy.BirthDate);
			}
			else if(sortBy == PersonSortType.DateCreated)
			{
				return _unitOfWork.PersonRepository.GetAll(page, eachPage, PersonSortBy.DateCreated);
			}

			throw new Exception(MessageException.SortByNotFound);
		}

		public PagedList<Person> GetProducers(int page, int eachPage, PersonSortType sortBy)
		{
			if(sortBy == PersonSortType.NamePerson)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.Role!.Equals(Role.RoleProducer), page, eachPage, PersonSortBy.NamePerson);
			}
			else if(sortBy == PersonSortType.BirthDate)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.Role!.Equals(Role.RoleProducer), page, eachPage, PersonSortBy.BirthDate);
			}
			else if(sortBy == PersonSortType.DateCreated)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.Role!.Equals(Role.RoleProducer), page, eachPage, PersonSortBy.DateCreated);
			}

			throw new Exception(MessageException.SortByNotFound);
		}

		public PagedList<Person> SearchByName(string name, int page, int eachPage, PersonSortType sortBy)
		{
			if(sortBy == PersonSortType.NamePerson)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.NamePerson!.Contains(name), page, eachPage, PersonSortBy.NamePerson);
			}
			else if(sortBy == PersonSortType.BirthDate)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.NamePerson!.Contains(name), page, eachPage, PersonSortBy.BirthDate);
			}
			else if(sortBy == PersonSortType.DateCreated)
			{
				return _unitOfWork.PersonRepository.GetAll(p => p.NamePerson!.Contains(name), page, eachPage, PersonSortBy.DateCreated);
			}

			throw new Exception(MessageException.SortByNotFound);
		}

		
	}
}
