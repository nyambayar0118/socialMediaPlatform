using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Reddit.WinForms.CustomControls
{
    public class VoteChangedEventArgs : EventArgs
    {
        public int VoteType { get; set; }
        public int NewVoteCount { get; set; } = 0;
        public uint PostId { get; set; }

        public VoteChangedEventArgs() { }
        public VoteChangedEventArgs(int voteType, int newVoteCount, uint postId)
        {
            VoteType = voteType;
            NewVoteCount = newVoteCount;
            PostId = postId;
        }
    }
}
