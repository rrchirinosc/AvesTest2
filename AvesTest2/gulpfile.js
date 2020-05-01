
"use strict";

var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    merge = require("merge-stream"),
    del = require("del"),
    sass = require("gulp-sass"),
    bundleconfig = require("./bundleconfig.json");

var paths = {
    scss: 'wwwroot/scss/'
};

var regex = {
    css: /\.css$/,
    html: /\.(html|htm)$/,
    js: /\.js$/
};


gulp.task("sass", function () {
    return gulp.src(paths.scss + '**/*.scss')
        .pipe(sass())
        .pipe(gulp.dest('wwwroot/css'));
});