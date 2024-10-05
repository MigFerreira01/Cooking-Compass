using CookingCompassAPI.Data.Context;
using CookingCompassAPI.Domain;
using CookingCompassAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingCompassAPI.Services.Implementations
{
    public class RatingService
    {

        private CookingCompassApiDBContext _cookingApiDBContext;

        private IRatingRepository _ratingRepository;

        public RatingService (CookingCompassApiDBContext cookingApiDBContext, IRatingRepository ratingRepository)
        {
            _cookingApiDBContext = cookingApiDBContext;
            _ratingRepository = ratingRepository;
        }

        public List<Rating> GetAll()
        {
            return _ratingRepository.GetAll();
        }

        public Rating GetById (int id) 
        {
            return _ratingRepository.GetById(id);
        }


        public Rating SaveRating (Rating rating)
        {
            bool RatingExists = _ratingRepository.GetAny(rating.Id);

            if (!RatingExists)
            {
               rating = _ratingRepository.Add(rating);
            }
            else
            {
               rating = _ratingRepository.Update(rating);
            }

            return rating;
        }

        public void RemoveRating (int id)
        {
            Rating ratingResult = _ratingRepository.GetById(id);

            if (ratingResult != null)
            {
                _ratingRepository.Remove(ratingResult);

                _cookingApiDBContext.SaveChanges();
            }
        }

    }




}
