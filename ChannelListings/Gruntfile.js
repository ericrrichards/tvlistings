module.exports = function (grunt) {
    'use strict';

    grunt.initConfig({
        //pkg: grunt.file.readJSON('package.json'),
        copy: {
            scripts: {
                expand: true,
                cwd: 'Content/Scripts/',
                src: '*.js',
                dest: 'bin/Debug/Content/Scripts'
            },
            styles: {
                expand: true,
                cwd: 'Content/css/',
                src: '*.css',
                dest: 'bin/Debug/Content/css'
            },
            images : {
                expand: true,
                cwd: 'Content/images/',
                src: '**',
                dest: 'bin/Debug/Content/images'
            },
            fonts : {
                expand: true,
                cwd: 'Content/fonts/',
                src: '**',
                dest: 'bin/Debug/Content/fonts'
            },
            views: {
                expand: true,
                cwd: 'Views/',
                src: '**/**',
                dest: 'bin/Debug/Views'
            }
        },
        watch: {
            scripts: {
                files: ['Content/Scripts/*.js', 'Views/*.cshtml', 'Content/css/*.css'],
                tasks: ['copy'],
                options: {
                    spawn: false
                }
            }
        }
        
    });
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.registerTask('default', ['copy']);
};