using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Application.DTOs.BrandDTOs;
using ECommerce.Domain.Repositories;

namespace ECommerce.Application.Services.Brand
{
    public class BrandService:IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public void Add(BrandDTO brandDTO)
        {
            var entered = _mapper.Map<Domain.Entities.Brand>(brandDTO);
            var brand = _brandRepository.Get(c => c.BrandName .Equals(entered.BrandName));
            if (brand.Equals(null))
            {
                _brandRepository.Add(_mapper.Map<Domain.Entities.Brand>(brandDTO));
            }
            throw new Exception("Already exist");
        }

        public void Delete(int id)
        {
            var brand = _brandRepository.Get(c => c.Id == id);
            if (brand.Equals(null))
            {
                throw new Exception("Not exist");
            }
            _brandRepository.Delete(brand);
        }

        public List<BrandDTO> GetAll()
        {
            var brandList = _brandRepository.GetAll();
            return _mapper.Map<List<BrandDTO>>(brandList);
        }

        public BrandDTO GetById(int id)
        {
            var brand = _brandRepository.Get(c => c.Id == id);
            return _mapper.Map<BrandDTO>(brand);
        }

        public void Update(int id, BrandDTO brandDTO)
        {
            var brand = _brandRepository.Get(c => c.Id == id);
            if (brand.Equals(null))
            {
                throw new Exception("Not exist");
            }
            var changed = _mapper.Map<Domain.Entities.Brand>(brandDTO);
            brand.BrandName = changed.BrandName;

            _brandRepository.Update(brand);
        }
    }
}
