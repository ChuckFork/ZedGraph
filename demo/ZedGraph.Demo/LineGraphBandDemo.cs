//============================================================================
//ZedGraph Class Library - A Flexible Charting Library for .Net
//Copyright (C) 2005 John Champion and Jerry Vos
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================
using System;
using System.Drawing;
using System.Collections;

using ZedGraph;

namespace ZedGraph.Demo
{
	/// <summary>
	/// Summary description for BarGraphBandDemo.
	/// </summary>
	public class LineGraphBandDemo : DemoBase
	{
		public LineGraphBandDemo() : base( "A demo of a bar graph with a region highlighted.",
											"Line Graph Band Demo", DemoType.Line )
		{
			GraphPane myPane = base.GraphPane;

			// Create a new graph with topLeft at (40,40) and size 600x400
			myPane.Title = "Line Graph with Band Demo";
			myPane.XAxis.Title = "Sequence";
			myPane.YAxis.Title = "Temperature, C";
			
			double[] y = { 100, 115, 75, 22, 98, 40, 10 };
			double[] y2 = { 90, 100, 95, 35, 80, 35, 35 };
			double[] y3 = { 80, 110, 65, 15, 54, 67, 18 };
			double[] x = { 100, 200, 300, 400, 500, 600, 700 };

			myPane.AxisFill = new Fill( Color.FromArgb( 255, 255, 245), Color.FromArgb( 255, 255, 190), 90F );

			// Generate a red bar with "Curve 1" in the legend
			LineItem myCurve = myPane.AddCurve( "Curve 1", x, y, Color.Red );
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a blue bar with "Curve 2" in the legend
			myCurve = myPane.AddCurve( "Curve 2", x, y2, Color.Blue );
			myCurve.Symbol.Fill = new Fill( Color.White );

			// Generate a green bar with "Curve 3" in the legend
			myCurve = myPane.AddCurve( "Curve 3", x, y3, Color.Green );
			myCurve.Symbol.Fill = new Fill( Color.White );

			myPane.ClusterScaleWidth = 100f;
			myPane.XAxis.Min = 0;
			myPane.XAxis.Max = 800;
			myPane.YAxis.IsShowGrid = true;
			myPane.YAxis.IsShowMinorGrid = true;

			BoxItem box = new BoxItem( new RectangleF( 0, 100, 800, 30 ), Color.Empty, 
					Color.FromArgb( 150, Color.LightGreen ) );
			box.Fill = new Fill( Color.White, Color.FromArgb( 200, Color.LightGreen ), 45.0F );
			box.ZOrder = ZOrder.E_BehindAxis;
			myPane.GraphItemList.Add( box );

			TextItem text = new TextItem( "Optimal\nRange", 750, 85, CoordType.AxisXYScale,
									AlignH.Right, AlignV.Center );
			text.FontSpec.Fill.IsVisible = false;
			text.FontSpec.Border.IsVisible = false;
			text.FontSpec.IsBold = true;
			text.FontSpec.IsItalic = true;
			myPane.GraphItemList.Add( text );
		}
	}
}
