﻿using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Interfaces
{
    public interface IAssignmentService
    {
        #region Command  
        public Task<GResponse<string>> AddAssignmentAsync(AddAssignmentRequest request);
        public Task<GResponse<string>> UpdatedAssignmentAsync(UpdatedAssignmentRequest request);
        public Task<GResponse<string>> DeleteAssignmentAsync(int Assignment_Id);

        #endregion

        #region Query  
        public Task<GResponse<IEnumerable<GetAssignmentResponseDto>>> GetAllAssignmentListAsync();
        public Task<GResponse<GetAssignmentResponseDto>> GetAssignmentByIdAsync();
        public Task<GResponse<GetAssignmentResponseDto>> GetPaginatedAssignmentListAsync(GetAssignmentPaginatedListRequest request);

        #endregion
    }
}
