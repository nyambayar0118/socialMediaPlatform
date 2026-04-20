using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Reddit.WinForms.CustomControls
{
    /// <summary>
    /// Хэрэглэгчийн хариу үйлдэл үзүүлэхэд үүсэх Custom Event
    /// </summary>
    public class VoteChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Хэрэглэгчийн дарсан хариу үйлдлийн төрөл
        /// </summary>
        public int VoteType { get; set; }
        /// <summary>
        /// Хэрэглэгчийн дарсан хариу үйлдлийн дараа очих тоон утга
        /// </summary>
        public int NewVoteCount { get; set; } = 0;
        /// <summary>
        /// Хэрэглэгчийн хариу үйлдэл дарж буй post-ийн ID дугаар
        /// </summary>
        public uint PostId { get; set; }

        /// <summary>
        /// Анхдагч байгуулагч
        /// </summary>
        public VoteChangedEventArgs() { }
        /// <summary>
        /// Параметртэй байгуулагч
        /// </summary>
        /// <param name="voteType">Хэрэглэгчийн дарсан хариу үйлдлийн төрөл</param>
        /// <param name="newVoteCount">Хэрэглэгчийн дарсан хариу үйлдлийн дараа очих тоон утга</param>
        /// <param name="postId">Хэрэглэгчийн хариу үйлдэл дарж буй post-ийн ID дугаар</param>
        public VoteChangedEventArgs(int voteType, int newVoteCount, uint postId)
        {
            VoteType = voteType;
            NewVoteCount = newVoteCount;
            PostId = postId;
        }
    }
}
