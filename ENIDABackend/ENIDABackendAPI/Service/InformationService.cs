using ENIDABackendAPI.Model;
using ENIDABackendAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ENIDABackendAPI.Service
{
    public class InformationService
    {
        private InformationRepository informationRepository;

        public InformationService(InformationRepository informationRepository)
        {
            this.informationRepository = informationRepository;
        }

        public List<Information> GetInformationForOffset(string imageId, int offset, int noOfPoints)
        {
            var allInformationForImage = informationRepository.GetInformationByImageIdOrderedByOffset(imageId).ToList();

            var index = allInformationForImage.FindIndex(info => info.YOffset == offset);

            var startIndex = getStartIndex(index, noOfPoints);

            if (IsAmountAvailableItemsShorterThanRequested(allInformationForImage, startIndex + noOfPoints))
            {
                noOfPoints = allInformationForImage.Count - startIndex;
            }

            return allInformationForImage
                .GetRange(startIndex, noOfPoints);
        }

        private int getStartIndex(int sourceIndex, int noOfRequestedItems)
        {
            int halfOfNoOfPoints = (int)Math.Floor(noOfRequestedItems / 2.0d);
            return Math.Max(0, sourceIndex - halfOfNoOfPoints);
        }

        private bool IsAmountAvailableItemsShorterThanRequested(List<Information> items, int requestedItems)
        {
            return requestedItems > items.Count;
        }
    }
}
