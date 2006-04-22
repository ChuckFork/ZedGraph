﻿//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright (C) 2004  John Champion
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
using System.Drawing.Drawing2D;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ZedGraph
{
	/// <summary>
	/// A class that represents a bordered and/or filled box (rectangle) object on
	/// the graph.  A list of
	/// BoxItem objects is maintained by the <see cref="GraphItemList"/> collection class.
	/// </summary>
	/// 
	/// <author> John Champion </author>
	/// <version> $Revision: 3.13 $ $Date: 2006-04-22 08:43:17 $ </version>
	[Serializable]
	public class BoxItem : GraphItem, ICloneable, ISerializable
	{
	#region Fields
		/// <summary>
		/// Private field that stores the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="BoxItem"/>.  Use the public property <see cref="Fill"/> to
		/// access this value.
		/// </summary>
		protected Fill fill;
		/// <summary>
		/// Private field that determines the properties of the border around this
		/// <see cref="BoxItem"/>
		/// Use the public property <see cref="Border"/> to access this value.
		/// </summary>
		protected Border border;
	#endregion

	#region Defaults
		/// <summary>
		/// A simple struct that defines the
		/// default property values for the <see cref="ArrowItem"/> class.
		/// </summary>
		new public struct Default
		{
			/// <summary>
			/// The default pen width used for the <see cref="BoxItem"/> border
            /// (<see cref="ZedGraph.Border.PenWidth"/> property).  Units are points (1/72 inch).
            /// </summary>
			public static float PenWidth = 1.0F;
			/// <summary>
			/// The default color used for the <see cref="BoxItem"/> border
			/// (<see cref="ZedGraph.Border.Color"/> property).
			/// </summary>
			public static Color BorderColor = Color.Black;
			/// <summary>
			/// The default color used for the <see cref="BoxItem"/> fill
			/// (<see cref="ZedGraph.Fill.Color"/> property).
			/// </summary>
			public static Color FillColor = Color.White;
		}
	#endregion

	#region Properties
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Fill"/> data for this
		/// <see cref="BoxItem"/>.
		/// </summary>
		public Fill	Fill
		{
			get { return fill; }
			set { fill = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="ZedGraph.Border"/> object, which
		/// determines the properties of the border around this
		/// <see cref="BoxItem"/>
		/// </summary>
		public Border Border
		{
			get { return border; }
			set { border = value; }
		}
	#endregion
	
	#region Constructors
		/// <overloads>Constructors for the <see cref="BoxItem"/> object</overloads>
		/// <summary>
		/// A constructor that allows the position, border color, and solid fill color
		/// of the <see cref="BoxItem"/> to be pre-specified.
		/// </summary>
		/// <param name="borderColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box border</param>
		/// <param name="fillColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box fill (will be a solid color fill)</param>
		/// <param name="rect">The <see cref="RectangleF" /> struct that defines
		/// the box.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame" />.</param>
		public BoxItem( RectangleF rect, Color borderColor, Color fillColor ) :
				base( rect.Left, rect.Top, rect.Width, rect.Height )
		{
			this.Border = new Border( borderColor, Default.PenWidth );
			this.Fill = new Fill( fillColor );
		}

		/// <summary>
		/// A constructor that allows the position
		/// of the <see cref="BoxItem"/> to be pre-specified.  Other properties are defaulted.
		/// </summary>
		/// <param name="rect">The <see cref="RectangleF"/> struct that defines
		/// the box.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		/// </param>
		public BoxItem( RectangleF rect ) :
			base( rect.Left, rect.Top, rect.Width, rect.Height )
		{
			this.Border = new Border( Default.BorderColor, Default.PenWidth );
			this.Fill = new Fill( Default.FillColor );
		}

		/// <summary>
		/// A default constructor that creates a <see cref="BoxItem"/> from a
		/// <see cref="RectangleF"/> of (0,0,1,1).  Other properties are defaulted.
		/// </summary>
		public BoxItem() : this( new RectangleF( 0, 0, 1, 1 ) )
		{
		}

		/// <summary>
		/// A constructor that allows the position, border color, and two-color
		/// gradient fill colors
		/// of the <see cref="BoxItem"/> to be pre-specified.
		/// </summary>
		/// <param name="borderColor">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the box border</param>
		/// <param name="fillColor1">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the start of the box gradient fill</param>
		/// <param name="fillColor2">An arbitrary <see cref="System.Drawing.Color"/> specification
		/// for the end of the box gradient fill</param>
		/// <param name="rect">The <see cref="RectangleF"/> struct that defines
		/// the box.  This will be in units determined by
		/// <see cref="ZedGraph.Location.CoordinateFrame"/>.
		/// </param>
		public BoxItem( RectangleF rect, Color borderColor,
							Color fillColor1, Color fillColor2 ) :
				base( rect.Left, rect.Top, rect.Width, rect.Height )
		{
			this.Border = new Border( borderColor, Default.PenWidth );
			this.Fill = new Fill( fillColor1, fillColor2 );
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="BoxItem"/> object from which to copy</param>
		public BoxItem( BoxItem rhs ) : base( rhs )
		{
			this.Border = rhs.Border.Clone();
			this.Fill = rhs.Fill.Clone();
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
		/// </summary>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Typesafe, deep-copy clone method.
		/// </summary>
		/// <returns>A new, independent copy of this class</returns>
		public BoxItem Clone()
		{
			return new BoxItem( this );
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema2 = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected BoxItem( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );

			fill = (Fill) info.GetValue( "fill", typeof(Fill) );
			border = (Border) info.GetValue( "border", typeof(Border) );
		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );
			info.AddValue( "schema2", schema2 );
			info.AddValue( "fill", fill );
			info.AddValue( "border", border );
		}
	#endregion
	
	#region Rendering Methods
		/// <summary>
		/// Render this object to the specified <see cref="Graphics"/> device.
		/// </summary>
		/// <remarks>
		/// This method is normally only called by the Draw method
		/// of the parent <see cref="GraphItemList"/> collection object.
		/// </remarks>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		override public void Draw( Graphics g, PaneBase pane, float scaleFactor )
		{
			// Convert the arrow coordinates from the user coordinate system
			// to the screen coordinate system
			RectangleF pixRect = this.Location.TransformRect( pane );

			if (	Math.Abs( pixRect.Left ) < 100000 &&
					Math.Abs( pixRect.Top ) < 100000 &&
					Math.Abs( pixRect.Right ) < 100000 &&
					Math.Abs( pixRect.Bottom ) < 100000 )
			{
				// If the box is to be filled, fill it
				this.fill.Draw( g, pixRect );

				// Draw the border around the box if required
				this.border.Draw( g, pane.IsPenWidthScaled, scaleFactor, pixRect );
			}
		}
		
		/// <summary>
		/// Determine if the specified screen point lies inside the bounding box of this
		/// <see cref="BoxItem"/>.
		/// </summary>
		/// <param name="pt">The screen point, in pixels</param>
		/// <param name="pane">
		/// A reference to the <see cref="PaneBase"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <returns>true if the point lies in the bounding box, false otherwise</returns>
		override public bool PointInBox( PointF pt, PaneBase pane, Graphics g, float scaleFactor )
		{
			// transform the x,y location from the user-defined
			// coordinate frame to the screen pixel location
			RectangleF pixRect = this.location.TransformRect( pane );

			return pixRect.Contains( pt );
		}
		
	#endregion
	
	}
}
