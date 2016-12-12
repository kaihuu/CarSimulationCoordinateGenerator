using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegmentInserter
{
    public partial class SegmentInserter : Form
    {
        public SegmentInserter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<SegmentData> resultSegment = new List<SegmentData>();
            List<LinkListData> resultLinkList = new List<LinkListData>();
            int id = 225; //通勤ルートのセマンティックリンクID
            //int startNum = 192180;//復路開始地点のリンクNUM
            int startNum = 1591573;//往路開始地点のリンクNUM
            DataTable LinkTable = DatabaseAccessor.LinkTableGetter2(id);
            DataRow[] LinkRows = LinkTable.Select(null, "NUM");
            DataRow[] StartLink = LinkTable.Select("NUM = "+ startNum);
            List<LinkData> linkList = new List<LinkData>();

            linkList.Add(new LinkData(Convert.ToString(StartLink[0]["LINK_ID"]), Convert.ToInt32(StartLink[0]["NUM"]),
                Convert.ToDouble(StartLink[0]["START_LAT"]), Convert.ToDouble(StartLink[0]["START_LONG"]),
                Convert.ToDouble(StartLink[0]["END_LAT"]), Convert.ToDouble(StartLink[0]["END_LONG"]), Convert.ToDouble(StartLink[0]["DISTANCE"])));
            //スタート地点のリンクを初期値に設定

            Boolean flag = true;
            int j = 0;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < LinkRows.Length; i++)
                {
                    if(Convert.ToDouble(LinkRows[i]["START_LAT"]) == linkList[j].END_LAT && Convert.ToDouble(LinkRows[i]["START_LONG"]) == linkList[j].END_LONG
                        && (Convert.ToDouble(LinkRows[i]["END_LAT"]) != linkList[j].START_LAT || Convert.ToDouble(LinkRows[i]["END_LONG"]) != linkList[j].START_LONG)){

                        linkList.Add(new LinkData(Convert.ToString(LinkRows[i]["LINK_ID"]), Convert.ToInt32(LinkRows[i]["NUM"]),
                Convert.ToDouble(LinkRows[i]["START_LAT"]), Convert.ToDouble(LinkRows[i]["START_LONG"]),
                Convert.ToDouble(LinkRows[i]["END_LAT"]), Convert.ToDouble(LinkRows[i]["END_LONG"]), Convert.ToDouble(LinkRows[i]["DISTANCE"])));
                        j++;
                        flag = true;
                        StateLabel.Text = Convert.ToString(linkList.Count);
                        StateLabel.Update();
                        break;
                }
                }

            }
            resultSegment = makeSegmentData(linkList,id);
            resultLinkList = makeLinkListData(linkList, id);
            DatabaseAccessor.InsertSegment(resultSegment);
            DatabaseAccessor.InsertLinkList(resultLinkList);
            StateLabel.Text = Convert.ToString(linkList.Count);
            StateLabel.Update();
        }
        private List<SegmentData> makeSegmentData(List<LinkData> linkList, int semanticLinkID)
        {
            List<SegmentData> result = new List<SegmentData>();
            double length = 100;//セグメント長さ
            double templength = length;
            double rest = linkList[0].DISTANCE;
            int segmentID = 1;
            string startLinkID = linkList[0].LINK_ID;
            int startNum = linkList[0].NUM;
            double startPointOffset = 0;
            int j = 0;
            double nextOffset=0;

                do
                {

                if(rest > length && templength == length)
                {
                    nextOffset += templength;
                    rest = rest - templength;
                    templength = length;
                    result.Add(new SegmentData(segmentID, semanticLinkID, startLinkID, startNum, startPointOffset));
                    segmentID++;
                    startLinkID = linkList[j].LINK_ID;
                    startNum = linkList[j].NUM;
                    startPointOffset = nextOffset;
                }
                    else if(rest > templength)
                    {
                    nextOffset = templength;
                        rest = rest - templength;
                        templength = length;
                        result.Add(new SegmentData(segmentID, semanticLinkID, startLinkID, startNum, startPointOffset));
                        segmentID++;
                        startLinkID = linkList[j].LINK_ID;
                        startNum = linkList[j].NUM;
                        startPointOffset = nextOffset;
                }

                    else if (rest == templength)
                    {
                        
                        rest = rest - templength;
                        templength = length;

                    result.Add(new SegmentData(segmentID, semanticLinkID, startLinkID, startNum, startPointOffset));
                        segmentID++;
                        j++;
                        startLinkID = linkList[j].LINK_ID;
                        startNum = linkList[j].NUM;
                    startPointOffset = 0;
                }

                    else
                    {
                        templength = templength - rest;
                        j++;
                    if (j >= linkList.Count)
                    {
                        result.Add(new SegmentData(segmentID, semanticLinkID, startLinkID, startNum, startPointOffset));
                        break;
                    }
                    rest = linkList[j].DISTANCE;

                    }
                    


                } while (true);




            return result;
        }
        private List<LinkListData> makeLinkListData(List<LinkData> linkList, int semanticLinkID)
        {
            List<LinkListData> result = new List<LinkListData>();
            double length = 100;//セグメント長さ
            double templength = length;
            double rest = linkList[0].DISTANCE;
            int segmentID = 1;
            string LinkID = linkList[0].LINK_ID;
            int linkNumber = 1;
            int j = 0;
            result.Add(new LinkListData(segmentID, semanticLinkID, linkNumber, LinkID));
            linkNumber++;
            do
            {


                if (rest > templength)
                {
                    rest = rest - templength;
                    templength = length;
                    if (LinkID != linkList[j].LINK_ID)
                    {
                        LinkID = linkList[j].LINK_ID;
                        result.Add(new LinkListData(segmentID, semanticLinkID, linkNumber, LinkID));
                        
                    }
                    segmentID++;
                    linkNumber = 1;
                   result.Add(new LinkListData(segmentID, semanticLinkID, linkNumber, LinkID));
                    linkNumber++;
                }

                else if (rest == templength)
                {

                    rest = rest - templength;
                    templength = length;
                    if (LinkID != linkList[j].LINK_ID)
                    {
                        LinkID = linkList[j].LINK_ID;
                        result.Add(new LinkListData(segmentID, semanticLinkID, linkNumber, LinkID));
                    }
                    segmentID++;
                    j++;
                    linkNumber = 1;
                    LinkID = linkList[j].LINK_ID;
                   result.Add(new LinkListData(segmentID, semanticLinkID, linkNumber, LinkID));
                   linkNumber++;
                }

                else
                {
                    templength = templength - rest;
                    j++;

                    if (j >= linkList.Count)
                    {

                        break;
                    }
                    rest = linkList[j].DISTANCE;
                    if (LinkID != linkList[j].LINK_ID)
                    {
                        LinkID = linkList[j].LINK_ID;
                        result.Add(new LinkListData(segmentID, semanticLinkID, linkNumber, LinkID));
                        linkNumber++;
                       
                    }

                }



            } while (true);




            return result;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    class LinkData
    {
        public string LINK_ID { get; set; }
        public int NUM { get; set; }
        public double START_LAT { get; set; }
        public double START_LONG { get; set; }
        public double END_LAT { get; set; }
        public double END_LONG { get; set; }

        public double DISTANCE { get; set; }

        public LinkData(String LINK_ID, int NUM, double START_LAT, double START_LONG, double END_LAT, double END_LONG, double DISTANCE)
        {
            this.LINK_ID = LINK_ID;
            this.NUM = NUM;
            this.START_LAT = START_LAT;
            this.START_LONG = START_LONG;
            this.END_LAT = END_LAT;
            this.END_LONG = END_LONG;
            this.DISTANCE = DISTANCE;
        }
    }
    class SegmentData
    {
        public int SEGMENT_ID { get; set; }
        public int SEMANTIC_LINK_ID { get; set; }
        public string START_LINK_ID { get; set; }
        public int START_NUM { get; set; }
        public double START_POINT_OFFSET { get; set; }

        public SegmentData(int SEGMENT_ID, int SEMANTIC_LINK_ID, string START_LINK_ID, int START_NUM, double START_POINT_OFFSET)
        {
            this.SEGMENT_ID = SEGMENT_ID;
            this.SEMANTIC_LINK_ID = SEMANTIC_LINK_ID;
            this.START_LINK_ID = START_LINK_ID;
            this.START_NUM = START_NUM;
            this.START_POINT_OFFSET = START_POINT_OFFSET;
        }
    }
    class LinkListData
    {
        public int SEGMENT_ID { get; set; }
        public int SEMANTIC_LINK_ID { get; set; }
        public int LINK_NUMBER { get; set; }
        public string LINK_ID { get; set; }
        public LinkListData(int SEGMENT_ID, int SEMANTIC_LINK_ID, int LINK_NUMBER, string LINK_ID)
        {
            this.SEGMENT_ID = SEGMENT_ID;
            this.SEMANTIC_LINK_ID = SEMANTIC_LINK_ID;
            this.LINK_ID = LINK_ID;
            this.LINK_NUMBER = LINK_NUMBER;
        }
    }
}
