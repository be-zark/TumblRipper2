using System;
using System.Collections.Generic;
using System.Globalization;

namespace TumblRipper2.classes
{
	public class DownloaderPriorityQueue
	{
		private readonly static int queues;

		private Queue<DownloadState>[] prioritylist = new Queue<DownloadState>[DownloaderPriorityQueue.queues];

		public List<DownloadState> flatlist = new List<DownloadState>();

		public int Count
		{
			get
			{
				int count = 0;
				Queue<DownloadState>[] queueArrays = this.prioritylist;
				for (int i = 0; i < (int)queueArrays.Length; i++)
				{
					count = count + queueArrays[i].Count;
				}
				return count;
			}
		}

		static DownloaderPriorityQueue()
		{
			DownloaderPriorityQueue.queues = 5;
		}

		public DownloaderPriorityQueue()
		{
			for (int i = 0; i < (int)this.prioritylist.Length; i++)
			{
				this.prioritylist[i] = new Queue<DownloadState>();
			}
		}

		internal void Clear()
		{
			this.flatlist.Clear();
			for (int i = 0; i < (int)this.prioritylist.Length; i++)
			{
				this.prioritylist[i].Clear();
			}
		}

		public DownloadState Dequeue()
		{
			DownloadState downloadState = null;
			for (int i = 0; i < (int)this.prioritylist.Length; i++)
			{
				Queue<DownloadState> downloadStates = this.prioritylist[i];
				if (downloadStates.Count > 0)
				{
					downloadState = downloadStates.Dequeue();
					lock (this.flatlist)
					{
						this.flatlist.Remove(downloadState);
					}
					return downloadState;
				}
			}
			return downloadState;
		}

		public void Enqueue(DownloadState d, int Priority)
		{
			this.prioritylist[Priority - 1].Enqueue(d);
			lock (this.flatlist)
			{
				this.flatlist.Add(d);
			}
		}

		public static int RandNumber(int Low, int High)
		{
			Guid guid = Guid.NewGuid();
			return (new Random(int.Parse(guid.ToString().Substring(0, 8), NumberStyles.HexNumber))).Next(Low, High);
		}
	}
}