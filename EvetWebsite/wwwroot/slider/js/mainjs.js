(function($) {

   "use strict";

    /*------------------------slider------------------------*/
    /* https://learn.jquery.com/using-jquery-core/document-ready/ */
    jQuery(document).ready(function() {

        /* initialize the slider based on the Slider's ID attribute from the wrapper above */
        jQuery('#rev_slider_1').show().revolution({
            parallax: {
                type: 'mouse+scroll',
                origo: 'slidercenter',
                speed: 400,
                levels: [5, 10, 15, 20, 25, 30, 35, 40,
                    45, 46, 47, 48, 49, 50, 51, 55
                ],
                disable_onmobile: 'on'
            },

            /* options are 'auto', 'fullwidth' or 'fullscreen' */
            sliderLayout: 'auto',
            /* RESPECT ASPECT RATIO */
            minHeight: '500',
            responsiveLevels: [1170, 1024, 778, 480],
            visibilityLevels: [1170, 1024, 778, 480],
            gridwidth: [1170, 1024, 778, 480],
            gridheight: [780, 780, 860, 720],

            /* basic navigation arrows and bullets */

            /* basic navigation arrows and bullets */
            navigation: {

                arrows: {
                    enable: true,
                    style: "zeus",
                    hide_onleave: false
                },


            }
        });
    });
    /*------------------------slider------------------------*/
    /* https://learn.jquery.com/using-jquery-core/document-ready/ */
    jQuery(document).ready(function() {

        /* initialize the slider based on the Slider's ID attribute from the wrapper above */
        jQuery('#rev_slider_2').show().revolution({
            parallax: {
                type: 'mouse+scroll',
                origo: 'slidercenter',
                speed: 400,
                levels: [5, 10, 15, 20, 25, 30, 35, 40,
                    45, 46, 47, 48, 49, 50, 51, 55
                ],
                disable_onmobile: 'on'
            },

            /* options are 'auto', 'fullwidth' or 'fullscreen' */
            sliderLayout: 'auto',
            /* RESPECT ASPECT RATIO */
            minHeight: '500',
            responsiveLevels: [1170, 1024, 778, 480],
            visibilityLevels: [1170, 1024, 778, 480],
            gridwidth: [1170, 1024, 778, 480],
            gridheight: [885, 780, 860, 720],

            /* basic navigation arrows and bullets */

            /* basic navigation arrows and bullets */
            navigation: {

                arrows: {
                    enable: true,
                    style: "zeus",
                    hide_onleave: false
                },


            }
        });
    });

})(window.jQuery);