import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

export const slideInOut = trigger('slideInOut', [
  state(
    'hidden',
    style({
      left: '-100%', // يخرج العنصر خارج الشاشة بدون أن يؤثر على الـ scroll
      opacity: 0,
      visibility: 'hidden', // يمنع التفاعل معه
    })
  ),
  state(
    'visible',
    style({
      left: '0', // يظهر العنصر في مكانه
      opacity: 1,
      visibility: 'visible',
    })
  ),
  transition('hidden => visible', animate('300ms ease-in-out')),
  transition('visible => hidden', animate('300ms ease-in-out')),
]);
