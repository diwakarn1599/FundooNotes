// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Models;
    using global::Repository.Context;
    using global::Repository.Interface;

    /// <summary>
    /// label repository class
    /// </summary>
    /// <seealso cref="Repository.Interface.ILabelRepository" />
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// The label context
        /// </summary>
        private readonly UserContext labelContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="labelContext">The label context.</param>
        public LabelRepository(UserContext labelContext)
        {
            this.labelContext = labelContext;
        }

        /// <summary>
        /// Adds the label to note.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// boolean of label added or not
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
        public bool AddLabeltoNote(LabelModel labelData)
        {
            try
            {
                var checkLabel = this.labelContext.Labels.Where(label => label.LabelName.Equals(labelData.LabelName) && labelData.UserId == label.UserId && label.NoteId != null).FirstOrDefault();
                if (checkLabel == null)
                {
                    this.labelContext.Labels.Add(labelData);
                    this.labelContext.SaveChanges();
                    var checkLabelExists = this.labelContext.Labels.Where(x => x.UserId == labelData.UserId && x.LabelName.Equals(labelData.LabelName) && x.NoteId == null).FirstOrDefault();
                    if (checkLabelExists == null)
                    {
                        labelData.LabelId = 0;
                        labelData.NoteId = null;
                        this.labelContext.Labels.Add(labelData);
                        this.labelContext.SaveChanges();
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the label to user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// boolean of label added or not
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
        public bool AddLabeltoUser(LabelModel labelData)
        {
            try
            {
                var checkLabelExists = this.labelContext.Labels.Where(x => x.UserId == labelData.UserId && x.LabelName.Equals(labelData.LabelName)).FirstOrDefault();
                if (checkLabelExists == null)
                {
                    this.labelContext.Labels.Add(labelData);
                    this.labelContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the label from note.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// boolean of label deleted or not
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
        public bool DeleteLabelFromNote(int labelId)
        {
            try
            {
                var getLabel = this.labelContext.Labels.Find(labelId);
                if (getLabel != null)
                {
                    this.labelContext.Labels.Remove(getLabel);
                    this.labelContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the label from user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// boolean of label deleted or not
        /// </returns>
        /// <exception cref="System.Exception">throws exception</exception>
        public bool DeleteLabelFromUser(LabelModel labelData)
        {
            try
            {
                var getAllLabels = this.labelContext.Labels.Where(x => x.UserId == labelData.UserId && x.LabelName.Equals(labelData.LabelName)).ToList();
                if (getAllLabels != null)
                {
                    this.labelContext.Labels.RemoveRange(getAllLabels);
                    this.labelContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Edits the name of the label.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// boolean of label edited or not
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
        public bool EditLabelName(LabelModel labelData)
        {
            try
            {
                var getLabelName = this.labelContext.Labels.Find(labelData.LabelId);
                var getAllLabels = this.labelContext.Labels.Where(x => x.UserId == labelData.UserId && x.LabelName.Equals(getLabelName.LabelName)).ToList();
                if (getAllLabels.Count > 0)
                {
                    getAllLabels.ForEach(i => i.LabelName = labelData.LabelName);
                    this.labelContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label notes.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// list of label notes
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
        public List<NotesModel> GetLabelNotes(LabelModel labelData)
        {
            try
            {
                var getAllLabels = (from label in this.labelContext.Labels
                                   join notes in this.labelContext.Notes on label.NoteId equals notes.NoteId
                                    where (label.UserId == labelData.UserId && label.LabelName.Equals(labelData.LabelName))
                                   select notes).ToList();
                if (getAllLabels.Count > 0)
                {
                    return getAllLabels;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the label of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// list of user labels
        /// </returns>
        /// <exception cref="System.Exception">Throws Exception</exception>
        public List<string> GetLabelofUser(int userId)
        {
            try
            {
                var getAllLabels = this.labelContext.Labels.Where(x => x.UserId == userId)
                                    .Select(i => i.LabelName)
                                    .Distinct()
                                    .ToList();
                if (getAllLabels.Count > 0)
                {
                    return getAllLabels;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
