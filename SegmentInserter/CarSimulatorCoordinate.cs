using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Device.Location;
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
		//StartInsertボタンをクリックすると開始
		private void button1_Click(object sender, EventArgs e)
		{
			Console.WriteLine("ばかやろう");
			#region 初期化
			List<SegmentData> resultSegment = new List<SegmentData>();
			List<LinkListData> resultLinkList = new List<LinkListData>();
			List<CoodinateData> resultCoodinate = new List<CoodinateData>();
			List<RealCarPositionMatchingData> resultrealcarmatching = new List<RealCarPositionMatchingData>();
			List<VirtualCarPositionData> resultCarPositionData = new List<VirtualCarPositionData>();


			int id = 0; //通勤ルートのセマンティックリンクID
			int tripid = 0;
			int NumofCar = 0;//シミュレーションで生成する車の数
			int carspeed = 60;//車の速度

			//フォームのテキストボックスからIDなどを取得
			//id = Convert.ToInt32(textBox1.Text);
			//tripid = Convert.ToInt32(textBox4.Text);
			//NumofCar = Convert.ToInt32(textBox5.Text);

			id = 298;
			//tripid = 11;
			NumofCar = 100;


			int startNum = 0;
			int endNum = 0;

			startNum = Convert.ToInt32(textBox2.Text);
			endNum = Convert.ToInt32(textBox3.Text);

			//startNum = 45668;//通勤セマンティックリンクのスタート地点指定
			//endNum = 894847;//通勤セマンティックリンクのエンド地点指定
			#endregion

			DataTable LinkTable = DatabaseAccessor.LinkTableGetter2(id);//セマンティックリンクIDからセマンティックリンクデータを取得

			DataRow[] LinkRows = LinkTable.Select(null, "DISTANCE");//LinkTableをカラムDISTANCEでソートしてDataRow配列に変換
			DataRow[] StartLink = LinkTable.Select("NUM = " + startNum);//通勤セマンティックリンクのスタート地点のリンク構成点データだけ取り出し
			List<LinkData> linkList = new List<LinkData>();//リンクデータリスト追加

			linkList.Add(new LinkData(Convert.ToString(StartLink[0]["LINK_ID"]), Convert.ToInt32(StartLink[0]["NUM"]),
				Convert.ToDouble(StartLink[0]["START_LAT"]), Convert.ToDouble(StartLink[0]["START_LONG"]),
				Convert.ToDouble(StartLink[0]["END_LAT"]), Convert.ToDouble(StartLink[0]["END_LONG"]), Convert.ToDouble(StartLink[0]["DISTANCE"])));//スタート地点のリンク構成点データをadd

			#region 通勤リンクデータ結合処理
			Boolean flag = true;//結合できたか否かを判定するフラグ
			int j = 0;//linkList配列の最後尾を示すindex
			while (flag)
			{
				flag = false;//Link結合がされなければtrueにならずにループ終了
				for (int i = 0; i < LinkRows.Length; i++)//LinkRowsから結合可能なLinkを探索
				{
					if ((Convert.ToDouble(LinkRows[i]["START_LAT"]) == linkList[j].END_LAT && Convert.ToDouble(LinkRows[i]["START_LONG"]) == linkList[j].END_LONG)
						&& (Convert.ToDouble(LinkRows[i]["END_LAT"]) != linkList[j].START_LAT || Convert.ToDouble(LinkRows[i]["END_LONG"]) != linkList[j].START_LONG))//linklistの最後尾の値に結合可能なlinkのみを追加する
					{


						linkList.Add(new LinkData(Convert.ToString(LinkRows[i]["LINK_ID"]), Convert.ToInt32(LinkRows[i]["NUM"]),
													  Convert.ToDouble(LinkRows[i]["START_LAT"]), Convert.ToDouble(LinkRows[i]["START_LONG"]),
													  Convert.ToDouble(LinkRows[i]["END_LAT"]), Convert.ToDouble(LinkRows[i]["END_LONG"]), Convert.ToDouble(LinkRows[i]["DISTANCE"])));//該当LinkをLinklistに追加
						j++;//linkListにデータが追加されたので最後尾をUpdate


						flag = true;//Link結合されたのでtrueに書き換え
						StateLabel.Text = Convert.ToString(linkList.Count);
						StateLabel.Update();
						break;//結合が終わったので次のループへ進む

					}



				}

			}
			#endregion



			#region ここは多分消していい
			//DataTable RunTable = DatabaseAccessor.RunTableGetter(id, tripid);      //走行データ取得
			//List<RunData> runList = new List<RunData>();

			//DataRow[] RunRows = RunTable.Select(null, "JST");//走行データを時間でソートしてDataRowに変換
			// for (int i = 0; i < RunRows.Length; i++)
			//{
			//    runList.Add(new RunData(Convert.ToInt32(RunRows[i]["TRIP_ID"]),Convert.ToString(RunRows[i]["JST"]), Convert.ToDouble(RunRows[i]["LATITUDE"]),
			//                                            Convert.ToDouble(RunRows[i]["LONGITUDE"])));//DataRow[]をList<RunData>に変換
			// }




			// resultrealcarmatching = MatchingCarPosition(linkList, runList);//Link上の実ログデータの位置を算出
			#endregion

			//makePositionDataを書き換えれば完了
			resultCarPositionData = virtualcarPositionData(linkList);//シミュレーションデータを生成

			//resultCoodinate = makeCoodinateData(linkList, resultCarPositionData);//Link上の位置から座標データに変換
			//WriteCsv(resultCoodinate, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text));//CSVに書き出し
			CsvWriter(resultCarPositionData);





		}





		private static void WriteCsv(List<CoodinateData> resultcoordinate, int semantici_id, int trip_id, int num, int distance)
		{

			for (int j = 0; j < num; j++)
			{
				List<CoodinateData> insertcoordinate = new List<CoodinateData>();
				for (int k = 0; k < resultcoordinate.Count; k++)
				{
					if (resultcoordinate[k].CAR_NUM == j)
					{
						insertcoordinate.Add(resultcoordinate[k]);
						Console.WriteLine(resultcoordinate[k].LATITUDE + "  " + resultcoordinate[k].JST);
					}
				}

				try
				{
					// appendをtrueにすると，既存のファイルに追記
					//         falseにすると，ファイルを新規作成する
					var append = false;
					// 出力用のファイルを開く
					using (var sw = new System.IO.StreamWriter(@"C: \Users\arinaga\Documents\GitHub\CarSimulationCoordinateGenerator\" + "sim_" + semantici_id + "_" + trip_id + "_" + num + "_" + distance + "M" + j + ".csv", append))
					{
						for (int i = 0; i < insertcoordinate.Count; ++i)

						{

							DateTime dt1 = DateTime.Parse(insertcoordinate[i].JST);
							sw.WriteLine("{0},{1},{2},{3}", dt1.ToString("yyyy-MM-dd HH:mm:ss.FFF"),/*dt1.Year + "-" + dt1.Month + "-" + dt1.Day+ " " +dt1.TimeOfDay*/dt1.ToString("yyyy-MM-dd HH:mm:ss.FFF"), insertcoordinate[i].LATITUDE, insertcoordinate[i].LONGITUDE);
						}
					}
				}
				catch (System.Exception e)
				{
					// ファイルを開くのに失敗したときエラーメッセージを表示
					System.Console.WriteLine(e.Message);
				}


			}
		}

		#region 新規csv出力メソッド
		private static void CsvWriter(List<VirtualCarPositionData> virtualCarPositionDataList)
		{
			int datacount;
			int index = 0;
			datacount = virtualCarPositionDataList.Count;
			DateTime dtBirth = DateTime.Now;
			for (index = 0; index < datacount; index++) {
				VirtualCarPositionData virtualCarPositionData;
				double lat2;
				double long2;
				int num2;
				
				virtualCarPositionData = virtualCarPositionDataList[index];
				lat2 = virtualCarPositionData.LATITUDE;
				long2 = virtualCarPositionData.LONGITUDE;
				num2 = virtualCarPositionData.NUM;
				Console.WriteLine(dtBirth.ToString("yyyy-MM-dd HH:mm:ss.FFF") + "," + dtBirth.ToString("yyyy-MM-dd HH:mm:ss.FFF" )+ "," + lat2 + "," + long2 + "," + num2);
				dtBirth = dtBirth.AddSeconds(1);
				

			}
			

		}
		#endregion



		private List<RealCarPositionMatchingData> MatchingCarPosition(List<LinkData> linkList, List<RunData> runList)   //実際の車の位置（今回は必要ない）
		{

			List<RealCarPositionMatchingData> result = new List<RealCarPositionMatchingData>();



			for (int k = 0; k < runList.Count; k++)
			{
				double minDist = 255;
				int tempNum = 0;

				string tempLinkId = null;
				double rawstartpointoffset = 0;
				double startpointoffset = 0;


				GeoCoordinate matchinggpsPoint = new GeoCoordinate();


				for (int i = 0; i < linkList.Count; i++)
				{        //各リンクに対して
					Vector2D linkStartEdge = new Vector2D(linkList[i].START_LAT, linkList[i].START_LONG);
					Vector2D linkEndEdge = new Vector2D(linkList[i].END_LAT, linkList[i].END_LONG);
					Vector2D GPSPoint = new Vector2D(runList[k].LATITUDE, runList[k].LONGITUDE);
					//       Console.Write(GPSPoint.x+","+GPSPoint.y+"\n");


					//線分内の最近傍点を探す
					Vector2D matchedPoint = Vector2D.nearest(linkStartEdge, linkEndEdge, GPSPoint);

					//最近傍点との距離
					double tempDist = Vector2D.distance(GPSPoint, matchedPoint);




					//リンク集合の中での距離最小を探す
					if (tempDist < minDist)
					{
						GeoCoordinate linkStart = new GeoCoordinate();
						linkStart.Latitude = linkList[i].START_LAT;
						linkStart.Longitude = linkList[i].START_LONG;
						GeoCoordinate gpsPoint = new GeoCoordinate();
						gpsPoint.Latitude = runList[k].LATITUDE;
						gpsPoint.Longitude = runList[k].LONGITUDE;
						GeoCoordinate linkEnd = new GeoCoordinate();
						linkEnd.Latitude = linkList[i].END_LAT;
						linkEnd.Longitude = linkList[i].END_LONG;

						minDist = tempDist;


						tempNum = linkList[i].NUM;
						tempLinkId = linkList[i].LINK_ID;

						rawstartpointoffset = HubenyDistanceCalculator.CalcHubenyFormula(linkEnd, gpsPoint);
						//    Console.WriteLine(rawstartpointoffset);

						matchinggpsPoint = HubenyDistanceCalculator.CalcCoordinateFromHubenyFormula(linkEnd, rawstartpointoffset, linkStart);
						//   Console.WriteLine(matchinggpsPoint.Longitude);


					}
				}

				result.Add(new RealCarPositionMatchingData(runList[k].TRIP_ID, runList[k].JST, matchinggpsPoint.Latitude, matchinggpsPoint.Longitude, tempLinkId, tempNum, rawstartpointoffset));
				Console.Write(k + "   " + runList[k].JST + "   " + matchinggpsPoint.Latitude + "   " + matchinggpsPoint.Longitude + "   " + tempLinkId + "   " + tempNum + "   " + rawstartpointoffset + "\n");
			}
			return result;
		}


		private List<CarPositionData> makePositionData(List<LinkData> linkList, List<RealCarPositionMatchingData> realcarposi, int Ncar) //Ncar は台数
		{
			List<CarPositionData> result = new List<CarPositionData>();
			//  double v_distance = Convert.ToInt32(textBox6.Text);         //車速
			double v_distance = 50 / 3;   //1秒おきに

			for (int i = 0; i < realcarposi.Count; i++)
			{
				Console.WriteLine("i = " + i);
				//  string LinkID = "";
				//  int tempNUM = realcarposi[i].NUM;
				int j = 0;

				//        result.Add(new CarPositionData(realcarposi[i].TRIP_ID, realcarposi[i].JST,0,realcarposi[i].LINK_ID,realcarposi[i].NUM,realcarposi[i].END_POINT_OFFSET));


				for (int k = 0; k < linkList.Count(); k++)
				{
					if (linkList[k].NUM == realcarposi[i].NUM)
					{
						j = k;

					}
				}


				double startPointOffset = realcarposi[i].START_POINT_OFFSET;
				Console.WriteLine(realcarposi[i].START_POINT_OFFSET);

				string LinkID = linkList[j].LINK_ID;
				int LinkNum = linkList[j].NUM;

				double rest = linkList[j].DISTANCE - realcarposi[i].START_POINT_OFFSET;
				double nextstartPointOffset = realcarposi[i].START_POINT_OFFSET;
				// double temp_v_distance = Convert.ToInt32(textBox6.Text);
				double temp_v_distance = 50 / 3;
				int carnumber = 0;

				do
				{

					if (rest > v_distance && temp_v_distance == v_distance)    //①リンクの残り長さrest＞一秒間の走行距離v_distance＆まだリンクを跨いでいない

					{
						nextstartPointOffset += temp_v_distance;
						rest = rest - temp_v_distance;
						temp_v_distance = v_distance;
						result.Add(new CarPositionData(realcarposi[i].TRIP_ID, realcarposi[i].JST, carnumber, LinkID, LinkNum, nextstartPointOffset));
						Console.WriteLine("0  " + realcarposi[i].TRIP_ID + "  " + realcarposi[i].JST + "  " + carnumber + "  " + LinkID + "  " + LinkNum + "  " + nextstartPointOffset);
						//  result.Add(new SegmentData(segmentID, semanticLinkID, startLinkID, startNum, startPointOffset));
						carnumber++;
						LinkID = linkList[j].LINK_ID;
						LinkNum = linkList[j].NUM;
						startPointOffset = nextstartPointOffset;

						//nextstartPointOffset = startPointOffset - v_distance;
						//result.Add(new CarPositionData(realcarposi[i].TRIP_ID, realcarposi[i].JST, carnumber, LinkID, tempNUM, nextstartPointOffset));
						//startPointOffset = nextstartPointOffset;

					}

					else if (rest > temp_v_distance)           //②リンクの残り長さ＞残距離&その車両について2回目以降
					{

						nextstartPointOffset = temp_v_distance;
						rest = rest - temp_v_distance;
						temp_v_distance = v_distance;
						result.Add(new CarPositionData(realcarposi[i].TRIP_ID, realcarposi[i].JST, carnumber, LinkID, LinkNum, nextstartPointOffset));
						Console.WriteLine("1  " + realcarposi[i].TRIP_ID + "  " + realcarposi[i].JST + "  " + carnumber + "  " + LinkID + "  " + LinkNum + "  " + nextstartPointOffset);
						carnumber++;
						LinkID = linkList[j].LINK_ID;
						LinkNum = linkList[j].NUM;

						startPointOffset = nextstartPointOffset;

						//j--;
						//if (j < 0) break;
						//nextstartPointOffset = linkList[j].DISTANCE - (v_distance - startPointOffset);
						//LinkID = linkList[j].LINK_ID;
						//tempNUM = linkList[j].NUM;
						//result.Add(new CarPositionData(realcarposi[i].TRIP_ID, realcarposi[i].JST, carnumber, LinkID, tempNUM, nextstartPointOffset));



					}

					else if (rest == temp_v_distance)      //③リンクの残り長さが車間距離と同じ　＝
					{



						j++;
						rest = linkList[j].DISTANCE;
						temp_v_distance = v_distance;

						LinkID = linkList[j].LINK_ID;
						LinkNum = linkList[j].NUM;

						startPointOffset = 0;
						nextstartPointOffset = v_distance;

						result.Add(new CarPositionData(realcarposi[i].TRIP_ID, realcarposi[i].JST, carnumber, LinkID, LinkNum, startPointOffset));


						Console.WriteLine("2  " + realcarposi[i].TRIP_ID + "  " + realcarposi[i].JST + "  " + carnumber + "  " + LinkID + "  " + LinkNum + "  " + nextstartPointOffset);
						carnumber++;



						//if (j < 0) break
						//nextstartPointOffset = linkList[j].DISTANCE;
						//LinkID = linkList[j].LINK_ID;
						//tempNUM = linkList[j].NUM;
						//result.Add(new CarPositionData(realcarposi[i].TRIP_ID, realcarposi[i].JST, carnumber, LinkID, tempNUM, nextstartPointOffset));
					}
					else
					{
						Console.WriteLine("3");               //   ④リンクの残り長さ＜車間距離

						temp_v_distance = temp_v_distance - rest;
						j++;

						rest = linkList[j].DISTANCE;


						LinkID = linkList[j].LINK_ID;
						LinkNum = linkList[j].NUM;

					}


				} while (carnumber < Ncar);
			}
			return result;
		}
		#region　新規生成メソッド
		private List<VirtualCarPositionData> virtualcarPositionData(List<LinkData> linkList)
		{
			List<VirtualCarPositionData> result = new List<VirtualCarPositionData>();
			VirtualCarPositionData virtualCarPositionData;
			double v = Convert.ToInt32(textBox4.Text);
			v = v * 1000 / 3600;
			double past = 0;
			double rest = 0;
			int restcount = 0;
			//リンクを跨いだ回数

			int posicount = 0;
			//位置を作った数

			int count = 100;
			double lat1  ;
			double long1 ;
			int num;
			double restv = 0;


			//result[0].LATITUDE = linkList[0].START_LAT;
			//result[0].LONGITUDE = linkList[0].START_LONG;
			lat1 = linkList[0].START_LAT;
			long1 = linkList[0].START_LONG;
			num = linkList[0].NUM;

			virtualCarPositionData = new VirtualCarPositionData(lat1, long1, num);
			result.Add(virtualCarPositionData);
			// result.Add(new VirtualCarPositionData(linkList[0].START_LAT, linkList[0].START_LONG, linkList[0].NUM));


			#region 一つ目

			if (linkList[0].DISTANCE > v) 
				//次のリンクに移動しない場合
				{
				//result[1].LATITUDE = (linkList[0].END_LAT - linkList[0].START_LAT) * v / linkList[0].DISTANCE;
				//result[1].LONGITUDE = (linkList[0].END_LONG - linkList[0].START_LONG) * v / linkList[0].DISTANCE;
				lat1 = ((linkList[0].END_LAT - linkList[0].START_LAT) * v / linkList[0].DISTANCE) + linkList[0].START_LAT;
				long1 = ((linkList[0].END_LONG - linkList[0].START_LONG) * v / linkList[0].DISTANCE)+ linkList[0].START_LONG;
				num = linkList[0].NUM;
				past = linkList[0].DISTANCE - v;

				virtualCarPositionData = new VirtualCarPositionData(lat1, long1, num);
				result.Add(virtualCarPositionData);




			}

			else{//次以降のnumに移動する場合
				rest = v - linkList[restcount].DISTANCE;
				restcount++;
				
				while (rest>linkList[restcount].DISTANCE)
				{

					rest = rest - linkList[restcount].DISTANCE;
					restcount++;

				}
				//result[1].LATITUDE = (((linkList[count1].END_LAT - linkList[count1].START_LAT) * rest / linkList[count1].DISTANCE) + linkList[count1].START_LAT);
				//result[1].LONGITUDE = (((linkList[count1].END_LONG - linkList[count1].START_LONG) * rest / linkList[count1].DISTANCE) + linkList[count1].START_LONG);
				lat1 = (((linkList[restcount].END_LAT - linkList[restcount].START_LAT) * rest / linkList[restcount].DISTANCE) + linkList[restcount].START_LAT);
				long1 = (((linkList[restcount].END_LONG - linkList[restcount].START_LONG) * rest / linkList[restcount].DISTANCE) + linkList[restcount].START_LONG);
				num = linkList[restcount].NUM;
				past = linkList[restcount].DISTANCE - rest;

				virtualCarPositionData = new VirtualCarPositionData(lat1, long1, num);
				result.Add(virtualCarPositionData);


			}

			#endregion


			#region　ループ
			while (restcount < (linkList.Count - 1))
			{
				if (linkList[restcount].DISTANCE - past > v)//次のnumに移動しない
				{
					lat1 = (linkList[restcount].END_LAT - linkList[restcount].START_LAT) * (v+past) / linkList[restcount].DISTANCE;
					long1 = (linkList[restcount].END_LONG - linkList[restcount].START_LONG) * (v+past) / linkList[restcount].DISTANCE;
					num = linkList[restcount].NUM;
					past = past + v;


					virtualCarPositionData = new VirtualCarPositionData(lat1, long1, num);
					result.Add(virtualCarPositionData);

					posicount++;
				}
				else//次のnumに移動する
				{
					
					rest = linkList[restcount].DISTANCE - past;
					restv = v - rest;
					restcount++;

					while (restv > linkList[restcount].DISTANCE)
					{
						restv = restv - linkList[restcount].DISTANCE;
						restcount++;
						
					}

					lat1 = (((linkList[restcount].END_LAT - linkList[restcount].START_LAT) * restv / linkList[restcount].DISTANCE) + linkList[restcount].START_LAT);
					long1 = (((linkList[restcount].END_LONG - linkList[restcount].START_LONG) * restv / linkList[restcount].DISTANCE) + linkList[restcount].START_LONG);
					num = linkList[restcount].NUM;
					past = linkList[restcount].DISTANCE - restv;



					virtualCarPositionData = new VirtualCarPositionData(lat1, long1, num);
					result.Add(virtualCarPositionData);

					posicount++;


				}
			}
			

			#endregion


			return result;



			#endregion




		}
		//#region　座標データ変換メソッド
		//private List<CoodinateData> makeCoodinateData(List<LinkData>linkList,List<VirtualCarPositionData>carPositionData)
		//      {
		//          int Num = 0;
		//          GeoCoordinate coordinate = new GeoCoordinate();
		//          GeoCoordinate LinkStartCoodinate  = new GeoCoordinate();
		//          GeoCoordinate LinkEndCoodinate = new GeoCoordinate();

		//          List<CoodinateData> result = new List<CoodinateData>();
		//          for (int i = 0; i < carPositionData.Count; i++)
		//          {
		//              Num = carPositionData[i].NUM;
		//              for(int k = 0; k < linkList.Count; k++) {
		//                  if (linkList[k].NUM == Num) {
		//                      LinkStartCoodinate.Latitude = linkList[k].START_LAT;
		//                      LinkStartCoodinate.Longitude = linkList[k].START_LONG;
		//                      LinkEndCoodinate.Latitude = linkList[k].END_LAT;
		//                      LinkEndCoodinate.Longitude = linkList[k].END_LONG;

		//                      break;

		//                   }

		//              }

		//              coordinate = HubenyDistanceCalculator.CalcCoordinateFromHubenyFormula(LinkEndCoodinate,carPositionData[i].START_POINT_OFFSET,LinkStartCoodinate);
		//              result.Add(new CoodinateData(carPositionData[i].TRIP_ID,carPositionData[i].CAR_NUM,coordinate.Longitude,coordinate.Latitude,carPositionData[i].JST));
		//              Console.WriteLine(carPositionData[i].TRIP_ID+" "+ carPositionData[i].CAR_NUM + " " + coordinate.Longitude + " " + coordinate.Latitude + " " + carPositionData[i].JST);

		//          }



		//          return result;

		//      }
		//#endregion




		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void SegmentInserter_Load(object sender, EventArgs e)
		{

		}
	}
	#region class
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
	class RunData
	{ public int TRIP_ID { get; set; }
		public string JST { get; set; }
		public double LATITUDE { get; set; }
		public double LONGITUDE { get; set; }


		public RunData(int TRIP_ID, String JST, double LATITUDE, double LONGITUDE)
		{
			this.TRIP_ID = TRIP_ID;
			this.JST = JST;
			this.LATITUDE = LATITUDE;
			this.LONGITUDE = LONGITUDE;

		}
	}

	class SegmentData
	{
		public int SEGMENT_ID { get; set; }
		public int SEMANTIC_LINK_ID { get; set; }
		public string START_LINK_ID { get; set; }
		public int START_NUM { get; set; }
		public double START_POINT_OFFSET { get; set; }

		//  public double ALTITUDE { get; set; }

		public SegmentData(int SEGMENT_ID, int SEMANTIC_LINK_ID, string START_LINK_ID, int START_NUM, double START_POINT_OFFSET)
		{
			this.SEGMENT_ID = SEGMENT_ID;
			this.SEMANTIC_LINK_ID = SEMANTIC_LINK_ID;
			this.START_LINK_ID = START_LINK_ID;
			this.START_NUM = START_NUM;
			this.START_POINT_OFFSET = START_POINT_OFFSET;
			//    this.ALTITUDE = ALTITUDE;
		}
	}

	class CoodinateData
	{
		public int TRIP_ID { get; set; }
		public int CAR_NUM { get; set; }
		public double LONGITUDE { get; set; }
		public double LATITUDE { get; set; }
		public string JST { get; set; }

		//  public double ALTITUDE { get; set; }

		public CoodinateData(int TRIP_ID, int CAR_NUM, double LONGITUDE, double LATITUDE, string JST)
		{
			this.TRIP_ID = TRIP_ID;
			this.CAR_NUM = CAR_NUM;
			this.LONGITUDE = LONGITUDE;
			this.LATITUDE = LATITUDE;
			this.JST = JST;

		}
	}


	class CarPositionData {

		public int TRIP_ID { get; set; }

		public string JST { get; set; }

		public int CAR_NUM { get; set; }
		public string LINK_ID { get; set; }

		public int NUM { get; set; }
		public double START_POINT_OFFSET { get; set; }

		public double LAT { get; set; }
		public double LONG { get; set; }



		//  public double ALTITUDE { get; set; }

		public CarPositionData(int TRIP_ID, string JST, int CAR_NUM, string LINK_ID, int NUM, double START_POINT_OFFSET)
		{
			this.TRIP_ID = TRIP_ID;
			this.JST = JST;

			this.CAR_NUM = CAR_NUM;
			this.LINK_ID = LINK_ID;
			this.NUM = NUM;
			this.START_POINT_OFFSET = START_POINT_OFFSET;
			this.LAT = LAT;
			this.LONG = LONG;


		}
	}


	#region　前の時の緯度経度その他
	class RealCarPositionMatchingData

	{
		public int TRIP_ID { get; set; }
		public string JST { get; set; }
		public double LATITUDE { get; set; }
		public double LONGITUDE { get; set; }
		public string LINK_ID { get; set; }
		public int NUM { get; set; }
		public double START_POINT_OFFSET { get; set; }


		//  public double ALTITUDE { get; set; }



		public RealCarPositionMatchingData(int TRIP_ID, string JST, double LATITUDE, double LONGITUDE, string LINK_ID, int NUM, double START_POINT_OFFSET)
		{
			this.TRIP_ID = TRIP_ID;
			this.JST = JST;
			this.LATITUDE = LATITUDE;
			this.LONGITUDE = LONGITUDE;
			this.LINK_ID = LINK_ID;
			this.NUM = NUM;
			this.START_POINT_OFFSET = START_POINT_OFFSET;


		}
	}
	#endregion

	#region　新規緯度経度その他
	class VirtualCarPositionData

	{


		public double LATITUDE { get; set; }
		public double LONGITUDE { get; set; }
		public int NUM { get; set; }



		public VirtualCarPositionData(double LATITUDE, double LONGITUDE, int NUM)
		{

			this.LATITUDE = LATITUDE;
			this.LONGITUDE = LONGITUDE;
			this.NUM = NUM;


		}

	}
	#endregion
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


	class Vector2D
	{
		public double x { get; set; }
		public double y { get; set; }

		public Vector2D(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		//点Pから線分ABへの最も近い点を探索する
		public static Vector2D nearest(Vector2D A, Vector2D B, Vector2D P)
		{
			Vector2D a = new Vector2D(B.x - A.x, B.y - A.y);
			Vector2D b = new Vector2D(P.x - A.x, P.y - A.y);
			double r = (a.x * b.x + a.y * b.y) / (a.x * a.x + a.y * a.y);

			if (r <= 0)
			{
				return A;
			}
			else if (r >= 1)
			{
				return B;
			}
			else
			{
				Vector2D result = new Vector2D(A.x + r * a.x, A.y + r * a.y);
				return result;
			}
		}

		//線分ABの長さ
		public static double distance(Vector2D A, Vector2D B)
		{
			return Math.Sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y));
		}

	}
}
	#endregion
