profile.template =
{
    init: function () {
        var locale = "en";

        if (navigator.userLanguage)
            locale = navigator.userLanguage;
        else if (navigator.language)
            locale = navigator.language;
        else
            locale = "en";

        moment.locale(locale);

        $('.gmfProfile .gmfBadge').each(function () {
            var $expires = $(this).find('div.gmfExpires span');

            if ($(this).data('expires')) {
                var expires = moment($(this).data('expires')); {
                    $expires.text(moment(expires.toDate()).format('D MMM YYYY'));
                }
            }

            var $tooltip = $(this).next('.gmfTooltip');

            var $tooltipAwarded = $tooltip.find('div.gmfAwarded span');
            var $tooltipExpires = $tooltip.find('div.gmfExpires span');

            if ($(this).data('awarded')) {
                var awarded = moment($(this).data('awarded')); {
                    $tooltipAwarded.text(moment(awarded.toDate()).format('D MMM YYYY'));
                }
            }
            if ($(this).data('expires')) {
                var expires = moment($(this).data('expires')); {
                    $tooltipExpires.text(moment(expires.toDate()).format('D MMM YYYY'));
                }
            }

            $(this).qtip({
                style: { classes: 'qtip-jtools' }, position: {
                    my: 'top center', at: 'bottom center', adjust: { y: 0 }
                }, content: { text: $tooltip }
            });
        });
    }
}