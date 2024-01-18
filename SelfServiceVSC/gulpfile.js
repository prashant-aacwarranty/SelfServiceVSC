"use strict";

var gulp = require("gulp"),
	concat = require("gulp-concat"),
	cssmin = require("gulp-cssmin"),
	uglify = require("gulp-uglify"),
	merge = require("merge-stream"),
	del = require("del"),
	bundleconfig = require("./bundleconfig.json");

var regex = {
	css: /\.css$/,
	js: /\.js$/
};

function getBundles(regexPattern)
{
	return bundleconfig.filter(function(bundle)
	{
		return bundle.outputFileName !== "copyonly.js" && regexPattern.test(bundle.outputFileName);
	});
}

function clean(resolve)
{
	var files = bundleconfig
		.filter(function(bundle)
		{
			return bundle.outputFileName !== "copyonly.js";
		})
		.map(function(bundle)
		{
			return bundle.outputFileName;
		})
		.concat(["wwwroot/lib/**", "!wwwroot/lib"]);
	del.sync(files);
	resolve();
}
gulp.task(clean);

function copy()
{
	var mergedStream = null;
	bundleconfig.forEach(function(bundle)
	{
		bundle.inputFiles.forEach(function(file)
		{
			if (!/^wwwroot/.test(file))
			{
				var task = gulp.src(file, { base: "node_modules" })
					.pipe(gulp.dest("wwwroot/lib"));
				if (mergedStream === null)
				{
					mergedStream = merge(task);
				} else
				{
					mergedStream.add(task);
				}
			}
		});
	});
	return mergedStream;
}
gulp.task(copy);

function minJs()
{
	var tasks = getBundles(regex.js).map(function(bundle)
	{
		return gulp.src(bundle.inputFiles, { base: "." })
			.pipe(concat(bundle.outputFileName))
			.pipe(uglify())
			.pipe(gulp.dest("."));
	});
	return merge(tasks);
}
gulp.task(minJs);

function minCss()
{
	var tasks = getBundles(regex.css).map(function(bundle)
	{
		return gulp.src(bundle.inputFiles, { base: "." })
			.pipe(concat(bundle.outputFileName))
			.pipe(cssmin())
			.pipe(gulp.dest("."));
	});
	return merge(tasks);
}
gulp.task(minCss);

gulp.task("Release", gulp.series(clean, copy, minJs, minCss));
gulp.task("Debug", gulp.series(clean, copy));
