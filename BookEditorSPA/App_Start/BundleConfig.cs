﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace BookEditorSPA
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
		
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/jquery.unobtrusive*",
				"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
				"~/Scripts/knockout-{version}.js",
				"~/Scripts/knockout.validation.js"));

			bundles.Add(new ScriptBundle("~/bundles/libs").Include(
				"~/Scripts/app/selecrPicker.js",
				"~/Scripts/app/toastr.min.js"));


			bundles.Add(new ScriptBundle("~/bundles/app").Include(
				"~/Scripts/sammy-{version}.js",
				"~/Scripts/app/common.js",			
				"~/Scripts/app/app.datamodel.js",
				"~/Scripts/app/app.viewmodel.js",
				"~/Scripts/app/home.viewmodel.js",
				"~/Scripts/app/_run.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(

				"~/Scripts/bootstrap-select.js",
				"~/Scripts/bootstrap.js",
				"~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/toastr.min.css",
				 "~/Content/bootstrap-select.min.css",
				 "~/Content/bootstrap.css",
				 "~/Content/Site.css"));
		}
	}
}
